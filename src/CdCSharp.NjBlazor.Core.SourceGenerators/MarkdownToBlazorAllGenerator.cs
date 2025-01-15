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
/// Generates a Blazor component based on markdown files. Requires markdown files with analyzer
/// additional file access enabled in properties.
/// </summary>
[Generator]
public class MarkdownToBlazorAllGenerator : IIncrementalGenerator
{
    private static readonly string[] AttributeNameMatch = { "MarkdownResourcesAll", "MarkdownResourcesAllAttribute" };

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register a syntax provider that filters for classes with the MarkdownResourcesAllAttribute
        IncrementalValuesProvider<INamedTypeSymbol?> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsClassWithMarkdownResourceAllAttribute(s),
                transform: static (ctx, _) => GetSemanticTarget(ctx))
            .Where(static m => m is not null);

        // Register an additional files provider for markdown files
        IncrementalValueProvider<ImmutableArray<(string ResourceName, string Content)>> markdownFiles = context.AdditionalTextsProvider
            .Where(static file => file.Path.EndsWith(".md", System.StringComparison.OrdinalIgnoreCase))
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
    /// Retrieves the semantic target (class symbol) for generation.
    /// </summary>
    /// <param name="context">
    /// The generator syntax context.
    /// </param>
    /// <returns>
    /// The class symbol if it has the attribute; otherwise, null.
    /// </returns>
    private static INamedTypeSymbol? GetSemanticTarget(GeneratorSyntaxContext context)
    {
        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
        SemanticModel model = context.SemanticModel;
        INamedTypeSymbol? classSymbol = model.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;

        if (classSymbol == null)
            return null;

        // Check if the class has the MarkdownResourcesAllAttribute
        foreach (AttributeData attribute in classSymbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() == "MarkdownResourcesAllAttribute")
            {
                return classSymbol;
            }
        }

        return null;
    }

    /// <summary>
    /// Determines whether the given syntax node is a class with the MarkdownResourcesAllAttribute.
    /// </summary>
    /// <param name="syntaxNode">
    /// The syntax node to check.
    /// </param>
    /// <returns>
    /// True if the node is a matching class; otherwise, false.
    /// </returns>
    private static bool IsClassWithMarkdownResourceAllAttribute(SyntaxNode syntaxNode)
    {
        return syntaxNode is ClassDeclarationSyntax classDecl &&
               classDecl.AttributeLists
                   .SelectMany(al => al.Attributes)
                   .Any(a => AttributeNameMatch.Contains(a.Name.ToString()));
    }

    /// <summary>
    /// Adds the BuildRenderTree method to the partial class declaration.
    /// </summary>
    /// <param name="partialClassDeclaration">
    /// The partial class declaration syntax.
    /// </param>
    /// <param name="resourceContent">
    /// The content of the markdown resource.
    /// </param>
    /// <returns>
    /// The modified partial class declaration.
    /// </returns>
    private ClassDeclarationSyntax AddBuildRenderTreeMethod(ClassDeclarationSyntax partialClassDeclaration, string resourceContent)
    {
        // Escape backslashes and quotes in the resource content to ensure valid string literals
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

    /// <summary>
    /// Executes the generation logic for each class with the MarkdownResourcesAllAttribute and
    /// corresponding markdown files.
    /// </summary>
    /// <param name="classes">
    /// The classes to process.
    /// </param>
    /// <param name="markdownFiles">
    /// The markdown files to process.
    /// </param>
    /// <param name="spc">
    /// The source production context.
    /// </param>
    private void Execute(ImmutableArray<INamedTypeSymbol> classes, ImmutableArray<(string ResourceName, string Content)> markdownFiles, SourceProductionContext spc)
    {
        if (classes.IsDefaultOrEmpty || markdownFiles.IsDefaultOrEmpty)
            return;

        try
        {
            // Iterate over each class symbol
            foreach (ISymbol? classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
            {
                // Retrieve the namespace
                string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

                // Generate partial classes for each markdown file
                foreach ((string resourceName, string content) in markdownFiles)
                {
                    string hintName = $"{SanitizeResourceName(resourceName)}.g.cs";
                    string partialClassCode = GeneratePartialClassCode(namespaceName, resourceName, content);
                    spc.AddSource(hintName, SourceText.From(partialClassCode, Encoding.UTF8));
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
    /// Generates the partial class code based on the namespace, resource name, and resource content.
    /// </summary>
    /// <param name="namespaceName">
    /// The namespace of the original class.
    /// </param>
    /// <param name="resourceName">
    /// The name of the markdown resource.
    /// </param>
    /// <param name="resourceContent">
    /// The content of the markdown resource.
    /// </param>
    /// <returns>
    /// The generated partial class code as a string.
    /// </returns>
    private string GeneratePartialClassCode(string namespaceName, string resourceName, string resourceContent)
    {
        string className = GetClassNameFromResourceName(resourceName);

        ClassDeclarationSyntax partialClassDeclaration = SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
            .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("ComponentBase")));

        partialClassDeclaration = AddBuildRenderTreeMethod(partialClassDeclaration, resourceContent);

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
    /// Extracts the class name from the resource name.
    /// </summary>
    /// <param name="resourceName">
    /// The resource file path.
    /// </param>
    /// <returns>
    /// The extracted class name.
    /// </returns>
    private string GetClassNameFromResourceName(string resourceName)
    {
        return resourceName.Substring(0, resourceName.Length - 3) // Remove ".md"
                           .Replace(".", "_")
                           .Split('\\')
                           .Last();
    }

    /// <summary>
    /// Sanitizes the resource name to create a valid class name.
    /// </summary>
    /// <param name="resourceName">
    /// The original resource name.
    /// </param>
    /// <returns>
    /// A sanitized string suitable for use as a class name.
    /// </returns>
    private string SanitizeResourceName(string resourceName) => $"{resourceName.Substring(0, resourceName.Length - 3).Replace(".", "_").Replace("\\", "_").Replace(":", "_")}";
}