using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace CdCSharp.NjBlazor.Core.SourceGenerators;

/// <summary>
/// Generates a Blazor component based on markdown files.
/// Requires markdown files with analyzer additional file access enabled in properties.
/// </summary>
[Generator]
public class MarkdownToBlazorComponentGenerator : IIncrementalGenerator
{
    private static readonly string[] AttributeNameMatch = { "MarkdownResourceComponent", "MarkdownResourceComponentAttribute" };

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register a syntax provider that filters for classes with the MarkdownResourceComponentAttribute
        IncrementalValuesProvider<INamedTypeSymbol?> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsClassWithMarkdownResourceComponentAttribute(s),
                transform: static (ctx, _) => GetSemanticTarget(ctx))
            .Where(static m => m is not null);

        // Register an additional files provider for markdown files
        IncrementalValueProvider<ImmutableArray<(string ResourceName, string Content)>> markdownFiles = context.AdditionalTextsProvider
            .Where(static file => file.Path.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            .Select(static (file, ct) => (
                ResourceName: file.Path,
                Content: file.GetText(ct)?.ToString().Replace("\"", "\"\"") ?? string.Empty))
            .Where(static resource => !string.IsNullOrEmpty(resource.Content))
            .Collect();

        // Collect classes into a single collection
        IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> collectedClasses = classDeclarations.Collect();

        // Combine the collected classes with the markdown files
        IncrementalValueProvider<(ImmutableArray<INamedTypeSymbol> Left, ImmutableArray<(string ResourceName, string Content)> Right)> combined = collectedClasses.Combine(markdownFiles);

        // Register the source output
        context.RegisterSourceOutput(combined, (spc, source) =>
        {
            ImmutableArray<INamedTypeSymbol> classes = source.Left;
            ImmutableArray<(string ResourceName, string Content)> markdowns = source.Right;

            Execute(classes, markdowns, spc);
        });
    }

    /// <summary>
    /// Executes the generation logic for each class with the MarkdownResourceComponentAttribute and corresponding markdown files.
    /// </summary>
    /// <param name="classes">The classes to process.</param>
    /// <param name="markdownFiles">The markdown files to process.</param>
    /// <param name="spc">The source production context.</param>
    private void Execute(ImmutableArray<INamedTypeSymbol> classes, ImmutableArray<(string ResourceName, string Content)> markdownFiles, SourceProductionContext spc)
    {
        if (classes.IsDefaultOrEmpty)
            return;

        try
        {
            foreach (INamedTypeSymbol? classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
            {
                // Extract attribute arguments
                AttributeData? attribute = classSymbol.GetAttributes().FirstOrDefault(a => AttributeNameMatch.Contains(a.AttributeClass?.Name));

                if (attribute == null)
                    continue;

                // Extract resourceName and assemblyName from attribute arguments
                string resourceNameFromAttribute = attribute.ConstructorArguments.Length > 0 ? attribute.ConstructorArguments[0].Value?.ToString() ?? string.Empty : string.Empty;
                string? assemblyName = attribute.ConstructorArguments.Length > 1 ? attribute.ConstructorArguments[1].Value?.ToString() : null;

                if (string.IsNullOrEmpty(resourceNameFromAttribute))
                {
                    spc.ReportDiagnostic(Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "ERROR3",
                            "Invalid Attribute Argument",
                            $"Class '{classSymbol.Name}' has an empty resource name in its MarkdownResourceComponentAttribute.",
                            "Generation",
                            DiagnosticSeverity.Warning,
                            isEnabledByDefault: true),
                        Location.None));
                    continue;
                }

                // Find the markdown file that matches resourceNameFromAttribute
                (string ResourceName, string Content) matchingMarkdown = markdownFiles.FirstOrDefault(m => IsMatchingResource(resourceNameFromAttribute, m.ResourceName));

                if (!string.IsNullOrEmpty(matchingMarkdown.ResourceName))
                {
                    // Use markdown file content
                    string partialClassCode = GeneratePartialClassCode(classSymbol, classSymbol.ContainingNamespace.ToDisplayString(), resourceNameFromAttribute, matchingMarkdown.Content, assemblyName);
                    string hintName = $"{classSymbol.Name}.MarkdownToBlazor.g.cs";
                    spc.AddSource(hintName, SourceText.From(partialClassCode, Encoding.UTF8));
                }
                else
                {
                    // Resource not found in additional files
                    spc.ReportDiagnostic(Diagnostic.Create(
                        new DiagnosticDescriptor(
                            "ERROR2",
                            "Resource Not Found",
                            $"Markdown resource '{resourceNameFromAttribute}' not found for class '{classSymbol.Name}'. Ensure that the markdown file is included as an additional file.",
                            "Generation",
                            DiagnosticSeverity.Warning,
                            isEnabledByDefault: true),
                        Location.None));
                    continue;
                }
            }
        }
        catch (Exception ex)
        {
            spc.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(
                    "ERROR1",
                    "Error generating code",
                    $"{ex.Message} - {ex.StackTrace}",
                    "Generation",
                    DiagnosticSeverity.Error,
                    isEnabledByDefault: true),
                Location.None));
        }
    }

    /// <summary>
    /// Determines whether the given syntax node is a class with the MarkdownResourceComponentAttribute.
    /// </summary>
    /// <param name="syntaxNode">The syntax node to check.</param>
    /// <returns>True if the node is a matching class; otherwise, false.</returns>
    private static bool IsClassWithMarkdownResourceComponentAttribute(SyntaxNode syntaxNode)
    {
        return syntaxNode is ClassDeclarationSyntax classDecl &&
               classDecl.AttributeLists
                   .SelectMany(al => al.Attributes)
                   .Any(a => AttributeNameMatch.Contains(a.Name.ToString()));
    }

    /// <summary>
    /// Retrieves the semantic target (class symbol) for generation.
    /// </summary>
    /// <param name="context">The generator syntax context.</param>
    /// <returns>The class symbol if it has the attribute; otherwise, null.</returns>
    private static INamedTypeSymbol? GetSemanticTarget(GeneratorSyntaxContext context)
    {
        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
        SemanticModel model = context.SemanticModel;
        INamedTypeSymbol? classSymbol = model.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;

        if (classSymbol == null)
            return null;

        // Check if the class has the MarkdownResourceComponentAttribute
        foreach (AttributeData attribute in classSymbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() == "MarkdownResourceComponentAttribute")
            {
                return classSymbol;
            }
        }

        return null;
    }

    /// <summary>
    /// Determines if the markdown file path matches the resource name specified in the attribute.
    /// </summary>
    /// <param name="resourceNameFromAttribute">The resource name from the attribute.</param>
    /// <param name="markdownFilePath">The markdown file path.</param>
    /// <returns>True if it matches; otherwise, false.</returns>
    private static bool IsMatchingResource(string resourceNameFromAttribute, string markdownFilePath) =>
        // Implement a matching logic based on your project's resource naming conventions.
        // For example, check if the markdownFilePath ends with the resourceNameFromAttribute.
        markdownFilePath.EndsWith(resourceNameFromAttribute, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Sanitizes the resource name to create a valid class name.
    /// </summary>
    /// <param name="resourceName">The original resource name.</param>
    /// <returns>A sanitized string suitable for use as a class name.</returns>
    private string SanitizeResourceName(string resourceName) => $"{resourceName.Substring(0, resourceName.Length - 3).Replace(".", "_").Replace("\\", "_").Replace(":", "_")}";

    /// <summary>
    /// Generates the partial class code based on the class symbol, namespace, resource name, resource content, and assembly name.
    /// </summary>
    /// <param name="classSymbol">The class symbol.</param>
    /// <param name="namespaceName">The namespace of the class.</param>
    /// <param name="resourceName">The name of the markdown resource.</param>
    /// <param name="resourceContent">The content of the markdown resource.</param>
    /// <param name="assemblyName">The name of the assembly where the resource is located (optional).</param>
    /// <returns>The generated partial class code as a string.</returns>
    private string GeneratePartialClassCode(INamedTypeSymbol classSymbol, string namespaceName, string resourceName, string resourceContent, string? assemblyName)
    {
        string className = classSymbol.Name;

        // Extract type parameter list if the class is generic
        TypeParameterListSyntax? typeParameterList = null;
        ClassDeclarationSyntax? declaringSyntax = classSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() as ClassDeclarationSyntax;
        if (declaringSyntax != null)
        {
            typeParameterList = declaringSyntax.TypeParameterList;
        }

        ClassDeclarationSyntax partialClassDeclaration = SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword));

        if (typeParameterList != null)
        {
            partialClassDeclaration = partialClassDeclaration.WithTypeParameterList(typeParameterList);
        }

        partialClassDeclaration = AddBuildRenderTreeMethod(partialClassDeclaration, resourceName, resourceContent, assemblyName);

        CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Components")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Components.Rendering")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Nj.Blazor.Markdown"))
            )
            .AddMembers(SyntaxFactory.FileScopedNamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
                .AddMembers(partialClassDeclaration));

        return compilationUnit.NormalizeWhitespace().ToFullString();
    }

    /// <summary>
    /// Adds the BuildRenderTree method to the partial class declaration.
    /// </summary>
    /// <param name="partialClassDeclaration">The partial class declaration syntax.</param>
    /// <param name="resourceName">The name of the resource.</param>
    /// <param name="resourceContent">The content of the resource.</param>
    /// <param name="assemblyName">The name of the assembly where the resource is located (optional).</param>
    /// <returns>The modified partial class declaration.</returns>
    private ClassDeclarationSyntax AddBuildRenderTreeMethod(ClassDeclarationSyntax partialClassDeclaration, string resourceName, string resourceContent, string? assemblyName)
    {
        // Escape quotes in the resource content to ensure valid string literals
        string escapedContent = resourceContent.Replace("\"", "\"\"");

        StatementSyntax methodContent = SyntaxFactory.ParseStatement($"builder.AddContent(0, MarkdownToRenderFragmentParser.ParseText(@\"{escapedContent}\"));");

        List<StatementSyntax> blockStatements =
        [
            methodContent
        ];

        MethodDeclarationSyntax buildRenderTreeMethod =
            SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "BuildRenderTree")
            .WithModifiers(SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.ProtectedKeyword),
                SyntaxFactory.Token(SyntaxKind.OverrideKeyword)))
            .WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SingletonSeparatedList<ParameterSyntax>(
                SyntaxFactory.Parameter(SyntaxFactory.Identifier("builder"))
                    .WithType(SyntaxFactory.ParseTypeName("RenderTreeBuilder")))))
            .WithBody(SyntaxFactory.Block(blockStatements));

        partialClassDeclaration = partialClassDeclaration.AddMembers(buildRenderTreeMethod);

        return partialClassDeclaration;
    }
}