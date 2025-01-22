using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Markdown.SourceGenerators;

/// <summary>
/// Generates a Blazor component based on markdown files. Requires markdown files with analyzer
/// additional file access enabled in properties.
/// </summary>
[Generator]
public class MarkdownToBlazorAllGenerator : IIncrementalGenerator
{
    private static readonly string[] AttributeNameMatch = ["MarkdownResourcesAll", "MarkdownResourcesAllAttribute"];

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Get all classes with the MarkdownResourcesAll attribute
        IncrementalValuesProvider<(ClassDeclarationSyntax ClassDeclaration, string Namespace)> classDeclarations =
            context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                    transform: static (ctx, _) => GetTargetForGeneration(ctx))
                .Where(static m => m.ClassDeclaration != null);

        // Get all .md files from additional files
        IncrementalValuesProvider<AdditionalText> markdownFiles =
            context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(".md"));

        // Combine the class declarations with markdown files
        IncrementalValueProvider<(ImmutableArray<(ClassDeclarationSyntax ClassDeclaration, string Namespace)> Classes,
            ImmutableArray<AdditionalText> MarkdownFiles)> combined =
            classDeclarations.Collect().Combine(markdownFiles.Collect());

        // Register the source output
        context.RegisterSourceOutput(combined,
            static (spc, source) => Execute(source.Classes, source.MarkdownFiles, spc));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node) =>
        node is ClassDeclarationSyntax { AttributeLists.Count: > 0 } classDeclaration &&
        classDeclaration.AttributeLists.Any(attributeList =>
            attributeList.Attributes.Any(attribute =>
                IsMarkdownResourceAllAttribute(attribute)));

    private static (ClassDeclarationSyntax ClassDeclaration, string Namespace) GetTargetForGeneration(GeneratorSyntaxContext context)
    {
        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
        string namespaceName = GetNamespaceName(classDeclaration);
        return (classDeclaration, namespaceName);
    }

    private static void Execute(
        ImmutableArray<(ClassDeclarationSyntax ClassDeclaration, string Namespace)> classes,
        ImmutableArray<AdditionalText> markdownFiles,
        SourceProductionContext context)
    {
        if (!classes.Any() || !markdownFiles.Any())
            return;

        try
        {
            string namespaceName = classes[0].Namespace;
            List<(string ResourceName, string Content)> foundResources = FindAllMdResources(markdownFiles);

            foreach ((string ResourceName, string Content) in foundResources)
            {
                string hintName = GetResultClassName(ResourceName);
                string partialClassCode = GeneratePartialClassCode(namespaceName, ResourceName, Content);
                context.AddSource($"{hintName}.g.cs", SourceText.From(partialClassCode, Encoding.UTF8));
            }
        }
        catch (Exception ex)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(
                    "ERROR1",
                    "Error generating code",
                    $"{ex.Message} - {ex.StackTrace}",
                    "Generation",
                    DiagnosticSeverity.Error,
                    true),
                Location.None));
        }
    }
    private static string GetShortName(string name) => string.Join("", Regex.Split(name, @"(?<!^)(?=[A-Z])").Select(v => string.Concat(v.Take(2))));

    private static string GetResultClassName(string resourceName)
    {
        string resultName = resourceName.Split('\\').Last();
        resultName = resultName.Replace(".", "_").Replace(":", "_").Replace("\\", "_").Replace("/", "_");

        return resultName;
    }

    private static List<(string ResourceName, string Content)> FindAllMdResources(
        ImmutableArray<AdditionalText> additionalFiles)
    {
        List<(string, string)> contents = [];
        foreach (AdditionalText additionalText in additionalFiles)
        {
            if (additionalText.Path.EndsWith(".md"))
            {
                SourceText? sourceText = additionalText.GetText();
                if (sourceText != null)
                {
                    contents.Add((additionalText.Path, sourceText.ToString().Replace("\"", "\"\"")));
                }
            }
        }
        return contents;
    }

    private static ClassDeclarationSyntax AddBuildRenderTreeMethod(
        ClassDeclarationSyntax partialClassDeclaration,
        string resourceContent)
    {
        StatementSyntax methodContent = SyntaxFactory.ParseStatement(
            $"builder.AddContent(0, MarkdownToRenderFragmentParser.ParseText(@\"{resourceContent}\"));");

        List<StatementSyntax> blockStatements = [methodContent];

        MethodDeclarationSyntax buildRenderTreeMethod =
            SyntaxFactory.MethodDeclaration(
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
                "BuildRenderTree")
            .WithModifiers(SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.ProtectedKeyword),
                SyntaxFactory.Token(SyntaxKind.OverrideKeyword)))
            .WithParameterList(SyntaxFactory.ParameterList(
                SyntaxFactory.SingletonSeparatedList<ParameterSyntax>(
                    SyntaxFactory.Parameter(SyntaxFactory.Identifier("builder"))
                        .WithType(SyntaxFactory.ParseTypeName("RenderTreeBuilder")))))
            .WithBody(SyntaxFactory.Block(blockStatements));

        return partialClassDeclaration.AddMembers(buildRenderTreeMethod);
    }

    private static string GeneratePartialClassCode(
        string namespaceName,
        string resourceName,
        string resourceContent)
    {
        string className = GetResultClassName(resourceName);

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
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Features.Markdown")))
            .AddMembers(
                SyntaxFactory.FileScopedNamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
                    .AddMembers(partialClassDeclaration));

        return compilationUnit.NormalizeWhitespace().ToFullString();
    }

    private static string GetNamespaceName(ClassDeclarationSyntax classDeclaration)
    {
        NamespaceDeclarationSyntax namespaceDeclaration = classDeclaration.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclaration = classDeclaration.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();

        if (namespaceDeclaration != null)
            return namespaceDeclaration.Name.ToString();
        else if (fileScopedNamespaceDeclaration != null)
            return fileScopedNamespaceDeclaration.Name.ToString();
        else
            return string.Empty;
    }

    private static bool IsMarkdownResourceAllAttribute(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax.Name is not IdentifierNameSyntax)
            return false;

        return AttributeNameMatch.Contains(((IdentifierNameSyntax)attributeSyntax.Name).Identifier.Text);
    }

    public static AttributeArgumentSyntax GetMarkdownResourceComponentAttributeFirstParameter(
        ClassDeclarationSyntax classDeclaration)
    {
        AttributeSyntax? attribute = classDeclaration.AttributeLists
            .SelectMany(list => list.Attributes)
            .FirstOrDefault(attr => AttributeNameMatch.Contains(attr.Name.ToString()));

        if (attribute != null)
        {
            AttributeArgumentSyntax? firstArgument = attribute.ArgumentList?.Arguments.FirstOrDefault();
            if (firstArgument != null)
                return firstArgument;

            throw new InvalidOperationException("MyAttribute found but no parameters.");
        }

        throw new InvalidOperationException("MyAttribute not found.");
    }

    public static AttributeArgumentSyntax? GetAssemblyNameAttributeSecondParameter(
        ClassDeclarationSyntax classDeclaration)
    {
        AttributeSyntax? attribute = classDeclaration.AttributeLists
            .SelectMany(list => list.Attributes)
            .FirstOrDefault(attr => AttributeNameMatch.Contains(attr.Name.ToString()));

        if (attribute != null)
        {
            if (attribute.ArgumentList?.Arguments.Count < 2)
                return null;

            AttributeArgumentSyntax? secondArgument = attribute.ArgumentList?.Arguments[1];
            if (secondArgument != null)
                return secondArgument;

            throw new InvalidOperationException("MyAttribute found null parameter value");
        }

        throw new InvalidOperationException("Attribute not found.");
    }
}