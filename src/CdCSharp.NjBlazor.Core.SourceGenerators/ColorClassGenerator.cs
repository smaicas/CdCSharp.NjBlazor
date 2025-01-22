using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

/// <summary>
/// Generates colors palette from System.Drawing.Color for the specific by <see
/// cref="AutogenerateCssColorsAttribute" /> class.
/// </summary>
[Generator]
public class ColorClassGenerator : IIncrementalGenerator
{
    private const int VariantLevels = 5;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register a syntax provider that filters for classes with the AutogenerateCssColorsAttribute
        IncrementalValuesProvider<INamedTypeSymbol?> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsClassWithAutogenerateCssColorsAttribute(s),
                transform: static (ctx, _) => GetSemanticTarget(ctx))
            .Where(static m => m is not null);

        // Combine all class declarations into a single collection
        IncrementalValueProvider<(Compilation Left, ImmutableArray<INamedTypeSymbol> Right)> compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        // Register the source output
        context.RegisterSourceOutput(compilationAndClasses, (spc, source) => Execute(source.Left, source.Right, spc));
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

        // Check if the class has the AutogenerateCssColorsAttribute
        foreach (AttributeData attribute in classSymbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString().Contains("AutogenerateCssColorsAttribute") == true ||
                attribute.AttributeClass?.ToDisplayString().Contains("AutogenerateCssColors") == true)
            {
                return classSymbol;
            }
        }

        return null;
    }

    /// <summary>
    /// Determines whether the given syntax node is a class with the AutogenerateCssColorsAttribute.
    /// </summary>
    /// <param name="syntaxNode">
    /// The syntax node to check.
    /// </param>
    /// <returns>
    /// True if the node is a matching class; otherwise, false.
    /// </returns>
    private static bool IsClassWithAutogenerateCssColorsAttribute(SyntaxNode syntaxNode)
    {
        bool classes = syntaxNode is ClassDeclarationSyntax classDecl &&
               classDecl.AttributeLists
                   .SelectMany(al => al.Attributes)
                   .Any(a => a.Name.ToString().Contains("AutogenerateCssColors"));
        return classes;
    }

    /// <summary>
    /// Executes the generation logic for each class with the AutogenerateCssColorsAttribute.
    /// </summary>
    /// <param name="compilation">
    /// The compilation context.
    /// </param>
    /// <param name="classes">
    /// The classes to process.
    /// </param>
    /// <param name="context">
    /// The source production context.
    /// </param>
    private void Execute(Compilation compilation, ImmutableArray<INamedTypeSymbol> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
            return;

        foreach (ISymbol? classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
        {
            // Get the namespace and class name
            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            string className = classSymbol.Name;

            // Get all the colors from System.Drawing.Color
            PropertyInfo[] colors = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);

            // Generate class declaration
            ClassDeclarationSyntax classSyntax = GenerateClassDeclaration(className);

            // Generate properties for each color
            foreach (PropertyInfo color in colors)
            {
                string propertyName = color.Name;
                ClassDeclarationSyntax innerClassDeclaration = GenerateInnerClassDeclaration(propertyName);

                PropertyDeclarationSyntax propertyDeclaration = GenerateColorProperty(propertyName);
                innerClassDeclaration = innerClassDeclaration.AddMembers(propertyDeclaration);

                for (int i = 1; i <= VariantLevels; i++)
                {
                    PropertyDeclarationSyntax variantDarkenPropertyDeclaration =
                        GenerateVariantPropertyDarken(propertyName, i);
                    innerClassDeclaration = innerClassDeclaration.AddMembers(variantDarkenPropertyDeclaration);
                }

                for (int i = 1; i <= VariantLevels; i++)
                {
                    PropertyDeclarationSyntax variantLightenPropertyDeclaration =
                        GenerateVariantPropertyLighten(propertyName, i);
                    innerClassDeclaration = innerClassDeclaration.AddMembers(variantLightenPropertyDeclaration);
                }

                classSyntax = classSyntax.AddMembers(innerClassDeclaration);
            }

            // Generate Enum with Variants
            EnumDeclarationSyntax variantsEnum = GenerateVariantsEnum();

            // Create the namespace
            NamespaceDeclarationSyntax namespaceDeclaration =
                SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
                    .WithNamespaceKeyword(
                        SyntaxFactory.Token(
                            SyntaxFactory.TriviaList(),
                            SyntaxKind.NamespaceKeyword,
                            "namespace",
                            "namespace",
                            SyntaxFactory.TriviaList(SyntaxFactory.Space)));

            // Create the compilation unit
            CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit()
                .AddUsings(
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Diagnostics.CodeAnalysis")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Drawing"))
                ).AddMembers(namespaceDeclaration
                    .AddMembers(classSyntax)
                );

            CompilationUnitSyntax colorVariantsCompilationUnit = SyntaxFactory.CompilationUnit()
                .AddMembers(namespaceDeclaration
                    .AddMembers(variantsEnum)
                );

            // Generate the source code
            string sourceCode = compilationUnit.NormalizeWhitespace().ToFullString();
            string colorVariantSource = colorVariantsCompilationUnit.NormalizeWhitespace().ToFullString();

            // Add the generated source code to the compilation
            context.AddSource($"{className}.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
            context.AddSource("ColorVariant.g.cs", SourceText.From(colorVariantSource, Encoding.UTF8));
        }
    }

    private ClassDeclarationSyntax GenerateClassDeclaration(string className) =>
        SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword),
                SyntaxFactory.Token(SyntaxKind.PartialKeyword));

    private PropertyDeclarationSyntax GenerateColorProperty(string name)
    {
        string cssColor = $"new CssColor(Color.{name})";

        return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("CssColor"), "Default")
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword))
            .WithAccessorList(
                SyntaxFactory.AccessorList(
                    SyntaxFactory.SingletonList(
                        SyntaxFactory.AccessorDeclaration(
                                SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(
                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)))))
            .WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(cssColor)))
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
    }

    private ClassDeclarationSyntax GenerateInnerClassDeclaration(string name) =>
        SyntaxFactory.ClassDeclaration(name)
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword));

    private PropertyDeclarationSyntax GenerateVariantPropertyDarken(string name, int index)
    {
        string cssColor = $"new CssColor(Color.{name}, ColorVariant.Darken{index})";

        return SyntaxFactory.PropertyDeclaration(
                SyntaxFactory.ParseTypeName("CssColor"), $"Darken{index}")
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword))
            .WithAccessorList(
                SyntaxFactory.AccessorList(
                    SyntaxFactory.SingletonList(
                        SyntaxFactory.AccessorDeclaration(
                                SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(
                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)))))
            .WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(cssColor)))
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
    }

    private PropertyDeclarationSyntax GenerateVariantPropertyLighten(string name, int index)
    {
        string cssColor = $"new CssColor(Color.{name}, ColorVariant.Lighten{index})";

        return SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("CssColor"), $"Lighten{index}")
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.StaticKeyword))
            .WithAccessorList(
                SyntaxFactory.AccessorList(
                    SyntaxFactory.SingletonList(
                        SyntaxFactory.AccessorDeclaration(
                                SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(
                                SyntaxFactory.Token(SyntaxKind.SemicolonToken)))))
            .WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(cssColor)))
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
    }

    private EnumDeclarationSyntax GenerateVariantsEnum()
    {
        EnumDeclarationSyntax factory = SyntaxFactory.EnumDeclaration("ColorVariant")
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        factory = factory.AddMembers(SyntaxFactory.EnumMemberDeclaration("Default"));
        for (int i = 1; i <= VariantLevels; i++)
        {
            factory = factory.AddMembers(SyntaxFactory.EnumMemberDeclaration($"Darken{i}"));
            factory = factory.AddMembers(SyntaxFactory.EnumMemberDeclaration($"Lighten{i}"));
        }

        return factory;
    }
}