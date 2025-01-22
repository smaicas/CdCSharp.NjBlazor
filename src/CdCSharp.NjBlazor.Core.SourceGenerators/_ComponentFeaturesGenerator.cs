//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Text;
//using System.Collections.Immutable;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;

//[Generator]
//public class ComponentFeaturesGenerator : IIncrementalGenerator
//{
//    public void Initialize(IncrementalGeneratorInitializationContext context)
//    {
//        IncrementalValuesProvider<INamedTypeSymbol?> classDeclarations = context.SyntaxProvider
//            .CreateSyntaxProvider(
//                predicate: static (s, _) => IsClassWithComponentFeaturesAttribute(s),
//                transform: static (ctx, _) => GetSemanticTarget(ctx))
//            .Where(static m => m is not null);

//        IncrementalValueProvider<(Compilation Left, ImmutableArray<INamedTypeSymbol?> Right)> compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

//        context.RegisterSourceOutput(compilationAndClasses, (spc, source) => Execute(source.Left, source.Right, spc));

//        context.RegisterSourceOutput(context.CompilationProvider, (spc, compilation) => spc.AddSource("ComponentFeaturesMarker.g.cs", SourceText.From(@"
//namespace ComponentFeaturesGenerated {
//    public static class Marker {
//        public const string IsGenerated = ""true"";
//    }
//}", Encoding.UTF8)));
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
//            if (attribute.AttributeClass?.Name == "ComponentFeaturesAttribute"
//                || attribute.AttributeClass?.Name == "ComponentFeatures")
//            {
//                return classSymbol;
//            }
//        }

//        return null;
//    }

//    private static bool IsClassWithComponentFeaturesAttribute(SyntaxNode syntaxNode)
//    {
//        return syntaxNode is ClassDeclarationSyntax classDecl &&
//               classDecl.AttributeLists
//                   .SelectMany(al => al.Attributes)
//                   .Any(a =>
//                       a.Name.ToString().Contains("ComponentFeaturesAttribute")
//                       || a.Name.ToString().Contains("ComponentFeatures"));
//    }

//    private void Execute(Compilation compilation, ImmutableArray<INamedTypeSymbol> classes, SourceProductionContext context)
//    {
//        if (classes.IsDefaultOrEmpty)
//            return;

//        foreach (ISymbol? classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
//        {
//            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
//            string className = classSymbol.Name;

//            foreach (AttributeData attribute in classSymbol.GetAttributes())
//            {
//                if (attribute.AttributeClass?.Name == "ComponentFeaturesAttribute")
//                {
//                    // Procesar cada tipo especificado en el atributo
//                    foreach (TypedConstant constructorArg in attribute.ConstructorArguments)
//                    {
//                        foreach (TypedConstant typeArg in constructorArg.Values)
//                        {
//                            if (typeArg.Value is INamedTypeSymbol featureSymbol)
//                            {
//                                SyntaxNode? syntaxNode = featureSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax();
//                                if (syntaxNode is ClassDeclarationSyntax originalClassSyntax)
//                                {
//                                    // Obtener el tipo base desde la clase genérica o simplemente usar "NjComponentBase"
//                                    string baseTypeName = classSymbol is INamedTypeSymbol namedClass ? namedClass.BaseType?.ToDisplayString() ?? "NjComponentBase" : "NjComponentBase";

//                                    // Generar la clase de la característica como parcial
//                                    ClassDeclarationSyntax featurePartialClass = CloneFeatureAsPartial(
//                                        (classSymbol as INamedTypeSymbol),
//                                        originalClassSyntax,
//                                        className,
//                                        baseTypeName);

//                                    // Generar el archivo fuente
//                                    CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit()
//                                        .AddUsings(
//                                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Core")),
//                                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Core.Abstractions.Components.Features")),
//                                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("CdCSharp.NjBlazor.Core.Abstractions.Components")),
//                                            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Components")))
//                                        .AddMembers(
//                                            SyntaxFactory.FileScopedNamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
//                                            .AddMembers(featurePartialClass));

//                                    string sourceCode = compilationUnit.NormalizeWhitespace().ToFullString();
//                                    context.AddSource($"{GetShortName(className)}_{GetShortName(featureSymbol.Name)}.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }

//    private string GetShortName(string name) => string.Join("", Regex.Split(name, @"(?<!^)(?=[A-Z])").Select(v => string.Concat(v.Take(2))));

//    private ClassDeclarationSyntax CloneFeatureAsPartial(
//        INamedTypeSymbol classSymbol,
//        ClassDeclarationSyntax originalClass,
//        string newClassName,
//        string featureBaseType)
//    {
//        // Cambiar el nombre de la clase de la característica y añadir el modificador 'partial'
//        ClassDeclarationSyntax partialClass = originalClass
//            .WithIdentifier(SyntaxFactory.Identifier(newClassName))
//            .WithModifiers(AdjustModifiers(originalClass.Modifiers));

//        // Ajustar la herencia para reflejar el parámetro de tipo de la característica
//        SimpleBaseTypeSyntax baseTypeSyntax = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(featureBaseType));
//        BaseListSyntax baseList = SyntaxFactory.BaseList(SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>(baseTypeSyntax));

//        if (classSymbol.TypeParameters.Length > 0)
//        {
//            partialClass = partialClass
//               .WithBaseList(baseList)
//               .WithTypeParameterList(SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList(classSymbol.TypeParameters.Select(tp => SyntaxFactory.TypeParameter(tp.Name)))));
//        }
//        else
//        {
//            partialClass = partialClass
//            .WithBaseList(baseList);
//        }
//        return partialClass;
//    }

//    private SyntaxTokenList AdjustModifiers(SyntaxTokenList originalModifiers)
//    {
//        // Mantener los modificadores 'abstract', 'public', etc.
//        if (originalModifiers.Any(SyntaxKind.AbstractKeyword))
//        {
//            return SyntaxFactory.TokenList(
//                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
//                SyntaxFactory.Token(SyntaxKind.AbstractKeyword),
//                SyntaxFactory.Token(SyntaxKind.PartialKeyword));
//        }

//        return SyntaxFactory.TokenList(
//            SyntaxFactory.Token(SyntaxKind.PublicKeyword),
//            SyntaxFactory.Token(SyntaxKind.PartialKeyword));
//    }
//}