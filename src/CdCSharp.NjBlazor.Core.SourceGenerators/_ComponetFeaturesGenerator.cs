//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Text;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;

//public class ComponentFeaturesGenerator : IComponentCodeGenerator
//{
//    public IncrementalValuesProvider<INamedTypeSymbol> ConfigureProvider(IncrementalGeneratorInitializationContext context)
//    {
//        return context.SyntaxProvider
//            .CreateSyntaxProvider(
//                predicate: static (s, _) => IsClassWithComponentFeaturesAttribute(s),
//                transform: static (ctx, _) => GetSemanticTarget(ctx))
//            .Where(static m => m is not null)!;
//    }

//    private static bool IsClassWithComponentFeaturesAttribute(SyntaxNode syntaxNode)
//    {
//        if (syntaxNode is not ClassDeclarationSyntax classDecl)
//            return false;

//        return classDecl.AttributeLists
//            .SelectMany(al => al.Attributes)
//            .Any(a => a.Name is GenericNameSyntax gns &&
//                     gns.Identifier.Text is "ComponentFeatures" or "ComponentFeaturesAttribute");
//    }

//    private static INamedTypeSymbol? GetSemanticTarget(GeneratorSyntaxContext context)
//    {
//        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
//        SemanticModel model = context.SemanticModel;
//        INamedTypeSymbol? classSymbol = model.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;

//        if (classSymbol == null)
//            return null;

//        foreach (AttributeData attribute in classSymbol.GetAttributes())
//        {
//            if (attribute.AttributeClass?.ToDisplayString().Contains("ComponentFeatures") == true)
//            {
//                return classSymbol;
//            }
//        }

//        return null;
//    }

//    public void Execute(GeneratorExecutionContext context)
//    {
//        if (context.Classes.IsDefaultOrEmpty)
//            return;

//        foreach (ISymbol? classSymbol in context.Classes.Distinct(SymbolEqualityComparer.Default))
//        {
//            ProcessClass(context, classSymbol);
//        }
//    }

//    private void ProcessClass(GeneratorExecutionContext context, ISymbol classSymbol)
//    {
//        string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
//        string className = classSymbol.Name;

//        IEnumerable<INamedTypeSymbol> featureSymbols = GetFeatureSymbols(classSymbol);

//        foreach (INamedTypeSymbol featureSymbol in featureSymbols)
//        {
//            GenerateFeatureClass(
//                context,
//                classSymbol,
//                featureSymbol,
//                namespaceName,
//                className);
//        }
//    }

//    private IEnumerable<INamedTypeSymbol> GetFeatureSymbols(ISymbol classSymbol)
//    {
//        return classSymbol.GetAttributes()
//            .Where(attr => attr.AttributeClass?.ToDisplayString().Contains("ComponentFeatures") == true)
//            .SelectMany(attr => attr.ConstructorArguments)
//            .SelectMany(arg => arg.Values)
//            .Select(arg => arg.Value)
//            .OfType<INamedTypeSymbol>();
//    }

//    private void GenerateFeatureClass(
//        GeneratorExecutionContext context,
//        ISymbol classSymbol,
//        INamedTypeSymbol featureSymbol,
//        string namespaceName,
//        string className)
//    {
//        // Get the feature class syntax
//        SyntaxNode? featureSyntaxNode = featureSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
//        if (featureSyntaxNode is not ClassDeclarationSyntax featureClassSyntax)
//            return;

//        // Get the original class declaration to get its modifiers
//        ClassDeclarationSyntax? originalClassDeclaration = classSymbol.DeclaringSyntaxReferences
//            .First()
//            .GetSyntax() as ClassDeclarationSyntax;

//        if (originalClassDeclaration == null)
//            return;

//        // Get usings from feature
//        CompilationUnitSyntax? featureCompilationUnit = featureClassSyntax.SyntaxTree.GetCompilationUnitRoot();
//        SyntaxList<UsingDirectiveSyntax> featureUsings = featureCompilationUnit.Usings;

//        // Get usings from original class
//        SyntaxList<UsingDirectiveSyntax> originalUsings = GetOriginalClassUsings(classSymbol);

//        string baseTypeName = classSymbol is INamedTypeSymbol namedClass
//                ? namedClass.BaseType?.Name ?? "NjComponentBase"
//                : "NjComponentBase";

//        // Combine original modifiers with required ones (public and partial)
//        IEnumerable<SyntaxToken> originalModifiers = originalClassDeclaration.Modifiers
//            .Where(m => m.Kind() != SyntaxKind.PartialKeyword); // Remove partial if exists

//        List<SyntaxToken> allModifiers = [];

//        // Add access modifier first (public or from original)
//        if (!originalModifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword) ||
//                                       m.IsKind(SyntaxKind.PrivateKeyword) ||
//                                       m.IsKind(SyntaxKind.ProtectedKeyword) ||
//                                       m.IsKind(SyntaxKind.InternalKeyword)))
//        {
//            allModifiers.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
//        }

//        // Add other modifiers from original class
//        allModifiers.AddRange(originalModifiers);

//        // Add partial modifier last
//        allModifiers.Add(SyntaxFactory.Token(SyntaxKind.PartialKeyword));

//        // Create partial class and preserve original members
//        ClassDeclarationSyntax partialClass = SyntaxFactory.ClassDeclaration(className)
//            .WithModifiers(SyntaxFactory.TokenList(allModifiers))
//            .WithBaseList(SyntaxFactory.BaseList(
//                SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(
//                    SyntaxFactory.SimpleBaseType(
//                        SyntaxFactory.ParseTypeName(baseTypeName)))))
//            .WithMembers(SyntaxFactory.List(
//                featureClassSyntax.Members.Where(m =>
//                    !m.IsKind(SyntaxKind.ConstructorDeclaration))));

//        // Rest of the code remains the same...
//        // Handle generic type parameters if present
//        if (classSymbol is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.TypeParameters.Length > 0)
//        {
//            partialClass = partialClass.WithTypeParameterList(
//                SyntaxFactory.TypeParameterList(
//                    SyntaxFactory.SeparatedList(
//                        namedTypeSymbol.TypeParameters.Select(tp =>
//                            SyntaxFactory.TypeParameter(tp.Name)))));
//        }

//        // Required usings
//        UsingDirectiveSyntax[] requiredUsings = new[]
//        {
//        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Core")),
//        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Core.Abstractions.Components.Features")),
//        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Core.Abstractions.Components")),
//        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Components"))
//    };

//        // Combine all usings
//        UsingDirectiveSyntax[] allUsings = featureUsings
//            .Concat(originalUsings)
//            .Concat(requiredUsings)
//            .GroupBy(u => u.Name.ToString())
//            .Select(g => g.First())
//            .ToArray();

//        CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit()
//            .AddUsings(allUsings)
//            .AddMembers(
//                SyntaxFactory.FileScopedNamespaceDeclaration(
//                    SyntaxFactory.ParseName(namespaceName))
//                .AddMembers(partialClass));

//        string sourceCode = compilationUnit.NormalizeWhitespace().ToFullString();
//        string fileName = $"{GetShortName(className)}_{GetShortName(featureSymbol.Name)}.g.cs";
//        context.AddSource(fileName, SourceText.From(sourceCode, Encoding.UTF8));
//    }

//    private SyntaxList<UsingDirectiveSyntax> GetOriginalClassUsings(ISymbol classSymbol)
//    {
//        // Get the original class syntax references
//        System.Collections.Immutable.ImmutableArray<SyntaxReference> syntaxReferences = classSymbol.DeclaringSyntaxReferences;
//        if (!syntaxReferences.Any())
//            return [];

//        // Get the first syntax reference (should be the main class declaration)
//        SyntaxNode syntaxNode = syntaxReferences.First().GetSyntax();

//        // Get the compilation unit to access usings
//        CompilationUnitSyntax compilationUnit = syntaxNode.SyntaxTree.GetCompilationUnitRoot();

//        return compilationUnit.Usings;
//    }

//    private string GetShortName(string name) =>
//        string.Join("", Regex.Split(name, @"(?<!^)(?=[A-Z])").Select(v => string.Concat(v.Take(2))));
//}