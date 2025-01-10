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
/// Generates the switch between variants for the classes described by ComponentDeMuxAttribute
/// </summary>
[Generator]
public class ComponentDeMuxGenerator : IIncrementalGenerator
{
    private static readonly string[] AttributeName = { "ComponentDeMux", "ComponentDeMuxAttribute" };

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register a syntax provider that filters for classes with the ComponentDeMuxAttribute
        IncrementalValuesProvider<INamedTypeSymbol?> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsClassWithComponentDeMuxAttribute(s),
                transform: static (ctx, _) => GetSemanticTarget(ctx))
            .Where(static m => m is not null);

        // Combine the compilation with the collected class symbols for generation
        IncrementalValueProvider<(Compilation Left, ImmutableArray<INamedTypeSymbol> Right)> compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

        // Register the source output
        context.RegisterSourceOutput(compilationAndClasses, (spc, source) => Execute(source.Left, source.Right, spc));
    }

    /// <summary>
    /// Executes the generation logic for each class with the ComponentDeMuxAttribute.
    /// </summary>
    /// <param name="compilation">The compilation context.</param>
    /// <param name="classes">The classes to process.</param>
    /// <param name="context">The source production context.</param>
    /// 
    private void Execute(Compilation compilation, ImmutableArray<INamedTypeSymbol> classes, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty)
            return;

        try
        {
            HashSet<string> processedClasses = new(); // Para evitar duplicados

            foreach (INamedTypeSymbol classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
            {
                // Obtén la declaración de sintaxis de la clase
                ClassDeclarationSyntax? classSyntax = classSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() as ClassDeclarationSyntax;
                if (classSyntax == null) continue;

                SemanticModel semanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
                string className = classSymbol.Name;

                // Verifica si ya procesamos esta clase
                if (processedClasses.Contains(className)) continue;
                processedClasses.Add(className);

                string namespaceName = GetNamespaceName(classSyntax);
                string partialClassCode = GeneratePartialClassCode(context, semanticModel, classSyntax, namespaceName);

                // Genera el archivo una sola vez
                context.AddSource($"{className}.ComponentDeMux.g.cs", SourceText.From(partialClassCode, Encoding.UTF8));
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
                    isEnabledByDefault: true),
                Location.None));
        }
    }
    //private void Execute(Compilation compilation, ImmutableArray<INamedTypeSymbol> classes, SourceProductionContext context)
    //{
    //    if (classes.IsDefaultOrEmpty)
    //        return;

    //    try
    //    {
    //        foreach (ISymbol? classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
    //        {
    //            foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
    //            {
    //                SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree);
    //                IEnumerable<ClassDeclarationSyntax> classDeclarations = syntaxTree.GetRoot().DescendantNodes()
    //                    .OfType<ClassDeclarationSyntax>()
    //                    .Where(classSyntax => classSyntax.AttributeLists.Any(attributeList =>
    //                        attributeList.Attributes.Any(attribute =>
    //                            IsComponentDeMuxAttribute(attribute))));

    //                foreach (ClassDeclarationSyntax? classDeclaration in classDeclarations)
    //                {
    //                    string namespaceName = GetNamespaceName(classDeclaration);
    //                    string partialClassCode = GeneratePartialClassCode(context, semanticModel, classDeclaration, namespaceName);
    //                    context.AddSource($"{classDeclaration.Identifier.Text}.ComponentDeMux.g.cs", SourceText.From(partialClassCode, Encoding.UTF8));
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        context.ReportDiagnostic(Diagnostic.Create(
    //            new DiagnosticDescriptor(
    //                "ERROR1",
    //                "Error generating code",
    //                $"{ex.Message} - {ex.StackTrace}",
    //                "Generation",
    //                DiagnosticSeverity.Error,
    //                isEnabledByDefault: true),
    //            Location.None));
    //    }
    //}

    /// <summary>
    /// Determines whether the given syntax node is a class with the ComponentDeMuxAttribute.
    /// </summary>
    /// <param name="syntaxNode">The syntax node to check.</param>
    /// <returns>True if the node is a matching class; otherwise, false.</returns>
    private static bool IsClassWithComponentDeMuxAttribute(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax classDecl)
        {
            IEnumerable<string> attrs = classDecl.AttributeLists
                   .SelectMany(al => al.Attributes)
                   .Select(a => a.Name.ToString());

            if (AttributeName.Any(expected => attrs.Any(a => a.Contains(expected))))
            {
                return true;
            };
        }
        return false;
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

        // Check if the class has the ComponentDeMuxAttribute
        foreach (AttributeData attribute in classSymbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString().Contains("ComponentDeMuxAttribute") == true
                || attribute.AttributeClass?.ToDisplayString().Contains("ComponentDeMux") == true)
            {
                return classSymbol;
            }
        }

        return null;
    }

    private static bool IsComponentDeMuxAttribute(AttributeSyntax attributeSyntax)
    {
        bool isGeneric = attributeSyntax.Name is GenericNameSyntax;
        if (isGeneric)
        {
            return AttributeName.Any(expected => attributeSyntax.Name.ToString().Contains(expected));
        }

        return false;
    }

    private string GetNamespaceName(ClassDeclarationSyntax classDeclaration)
    {
        NamespaceDeclarationSyntax? namespaceDeclaration = classDeclaration.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        FileScopedNamespaceDeclarationSyntax? fileScopedNamespaceDeclaration = classDeclaration.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();

        if (namespaceDeclaration != null)
        {
            return namespaceDeclaration.Name.ToString();
        }
        else if (fileScopedNamespaceDeclaration != null)
        {
            return fileScopedNamespaceDeclaration.Name.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private string GeneratePartialClassCode(SourceProductionContext context, SemanticModel semanticModel, ClassDeclarationSyntax originalClassDeclaration, string namespaceName)
    {
        string className = originalClassDeclaration.Identifier.Text;

        TypeParameterListSyntax? typeParameterList = originalClassDeclaration.TypeParameterList;

        ClassDeclarationSyntax partialClassDeclaration = SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
            .WithTypeParameterList(typeParameterList);

        partialClassDeclaration = AddVariantEnumDependentCode(context, semanticModel, originalClassDeclaration, partialClassDeclaration);

        partialClassDeclaration = AddLifeCycleMethodOverrides(partialClassDeclaration);

        // using Microsoft.AspNetCore.Components; using Microsoft.AspNetCore.Components.Rendering;
        CompilationUnitSyntax compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Components")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Components.Rendering")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName($"{namespaceName}.Variants"))
            )
            .AddMembers(SyntaxFactory.FileScopedNamespaceDeclaration(SyntaxFactory.ParseName(namespaceName))
                .AddMembers(partialClassDeclaration));

        return compilationUnit.NormalizeWhitespace().ToFullString();
    }

    private ClassDeclarationSyntax AddVariantEnumDependentCode(
        SourceProductionContext context,
        SemanticModel semanticModel,
        ClassDeclarationSyntax originalClassDeclaration,
        ClassDeclarationSyntax partialClassDeclaration)
    {
        IEnumerable<AttributeSyntax> attributes = originalClassDeclaration.AttributeLists.SelectMany(al => al.Attributes);

        AttributeSyntax? attribute = attributes.FirstOrDefault(IsComponentDeMuxAttribute);

        if (attribute == null)
        {
            throw new ArgumentException("Attribute not found.");
        }

        IdentifierNameSyntax? enumTypeIdentifier = attribute.DescendantNodes()
            .OfType<TypeArgumentListSyntax>()
            .FirstOrDefault()?.Arguments.FirstOrDefault() as IdentifierNameSyntax;

        if (enumTypeIdentifier == null)
        {
            throw new ArgumentException("Enum type identifier not found.");
        }

        SymbolInfo enumSymbol = semanticModel.GetSymbolInfo(enumTypeIdentifier);

        if (enumSymbol.Symbol is not ITypeSymbol typeSymbol)
        {
            throw new ArgumentException("Enum type symbol not found.");
        }

        EnumDeclarationSyntax? enumDeclaration = FindEnumDeclaration(compilation: semanticModel.Compilation, typeSymbol: typeSymbol);

        if (enumDeclaration == null)
        {
            throw new ArgumentException("Enum declaration not found.");
        }

        IEnumerable<string> enumNames = enumDeclaration.Members.Select(m => m.Identifier.Text);

        partialClassDeclaration = AddVariantParameter(partialClassDeclaration, enumTypeIdentifier, enumNames);

        partialClassDeclaration = AddBuildRenderTreeMethod(semanticModel, originalClassDeclaration, partialClassDeclaration, enumTypeIdentifier, enumNames);

        return partialClassDeclaration;
    }

    private EnumDeclarationSyntax? FindEnumDeclaration(Compilation compilation, ITypeSymbol typeSymbol)
    {
        foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
        {
            SyntaxNode root = syntaxTree.GetRoot();
            SemanticModel sm = compilation.GetSemanticModel(syntaxTree);

            foreach (EnumDeclarationSyntax enumDeclaration in root.DescendantNodes().OfType<EnumDeclarationSyntax>())
            {
                INamedTypeSymbol? symbol = sm.GetDeclaredSymbol(enumDeclaration);
                if (SymbolEqualityComparer.Default.Equals(symbol, typeSymbol))
                {
                    return enumDeclaration;
                }
            }
        }

        return null;
    }

    private ClassDeclarationSyntax AddVariantParameter(
        ClassDeclarationSyntax partialClassDeclaration,
        IdentifierNameSyntax enumTypeIdentifier,
        IEnumerable<string> enumNames)
    {
        PropertyDeclarationSyntax variantProperty = SyntaxFactory.PropertyDeclaration(
                    SyntaxFactory.ParseTypeName(enumTypeIdentifier.Identifier.Text), "Variant")
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithAttributeLists(SyntaxFactory.SingletonList(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Attribute(SyntaxFactory.ParseName("Parameter"))))))
                    .WithAccessorList(SyntaxFactory.AccessorList(
                        SyntaxFactory.List(new[]
                        {
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        })))
                    .WithInitializer(
                        SyntaxFactory.EqualsValueClause(
                            SyntaxFactory.ParseExpression($"{enumTypeIdentifier.Identifier.Text}.{enumNames.First()}")))
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

        partialClassDeclaration = partialClassDeclaration.AddMembers(variantProperty);
        return partialClassDeclaration;
    }

    private ClassDeclarationSyntax AddBuildRenderTreeMethod(
        SemanticModel semanticModel,
        ClassDeclarationSyntax originalClassDeclaration,
        ClassDeclarationSyntax partialClassDeclaration,
        IdentifierNameSyntax enumTypeIdentifier,
        IEnumerable<string> enumNames)
    {
        List<SwitchSectionSyntax> switchSections = [];

        // Iterate over enum names to create SwitchSections
        foreach (string name in enumNames)
        {
            CaseSwitchLabelSyntax caseSwitchLabel = SyntaxFactory.CaseSwitchLabel(
                SyntaxFactory.ParseExpression($"{enumTypeIdentifier.Identifier.Text}.{name}")
            );

            TypeParameterListSyntax? typeParameterList = originalClassDeclaration.TypeParameterList;
            string identifierString = string.Empty;
            if (typeParameterList != null)
            {
                string typeParametersString = string.Join(",", typeParameterList.Parameters.Select(p => p.Identifier.Text));
                identifierString = $"{originalClassDeclaration.Identifier.Text}Variant{name}<{typeParametersString}>";
            }
            else
            {
                identifierString = $"{originalClassDeclaration.Identifier.Text}Variant{name}";
            }

            IdentifierNameSyntax componentTypeSyntax = SyntaxFactory.IdentifierName(identifierString);

            TypeArgumentListSyntax typeArgumentList = SyntaxFactory.TypeArgumentList(
                SyntaxFactory.SingletonSeparatedList<TypeSyntax>(componentTypeSyntax)
            );

            ArgumentSyntax argument = SyntaxFactory.Argument(SyntaxFactory.ParseExpression("0"));

            ArgumentListSyntax argumentList = SyntaxFactory.ArgumentList(
                SyntaxFactory.SingletonSeparatedList(argument)
            );

            InvocationExpressionSyntax invocationExpression = SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("builder"),
                    SyntaxFactory.GenericName("OpenComponent")
                        .WithTypeArgumentList(typeArgumentList)
                )
            ).WithArgumentList(argumentList);

            ExpressionStatementSyntax expressionStatement = SyntaxFactory.ExpressionStatement(invocationExpression);

            BreakStatementSyntax breakStatement = SyntaxFactory.BreakStatement();

            BlockSyntax block = SyntaxFactory.Block(
                SyntaxFactory.List<StatementSyntax>(new StatementSyntax[] { expressionStatement, breakStatement })
            );

            SwitchSectionSyntax switchSection = SyntaxFactory.SwitchSection(
                SyntaxFactory.SingletonList<SwitchLabelSyntax>(caseSwitchLabel),
                SyntaxFactory.SingletonList<StatementSyntax>(block)
            );

            switchSections.Add(switchSection);
        }

        ExpressionSyntax memberAccessExpression = SyntaxFactory.ParseExpression("Variant");

        SwitchStatementSyntax switchStatement = SyntaxFactory.SwitchStatement(
            memberAccessExpression,
            SyntaxFactory.List(switchSections));

        INamedTypeSymbol? originalClassSymbolInfo = semanticModel.GetDeclaredSymbol(originalClassDeclaration);

        StatementSyntax closeComponentStatement = SyntaxFactory.ParseStatement("builder.CloseComponent();");

        List<StatementSyntax> blockStatements =
        [
            switchStatement,
            .. GetParameterAttributeStatements(originalClassSymbolInfo ?? throw new ArgumentException("originalClassSymbolInfo not found in semantic model.")),
            closeComponentStatement,
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

    private ClassDeclarationSyntax AddLifeCycleMethodOverrides(ClassDeclarationSyntax partialClassDeclaration)
    {
        foreach (string methodName in GetLifecycleMethodNames())
        {
            MethodDeclarationSyntax methodDeclaration;

            TypeSyntax returnType = methodName.Contains("Async")
                ? SyntaxFactory.ParseTypeName("Task")
                : SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));

            if (methodName is "OnAfterRender" or "OnAfterRenderAsync")
            {
                methodDeclaration = SyntaxFactory.MethodDeclaration(
                    returnType,
                    methodName)
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.ProtectedKeyword),
                            SyntaxFactory.Token(SyntaxKind.OverrideKeyword)))
                    .WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SingletonSeparatedList<ParameterSyntax>(
                        SyntaxFactory.Parameter(SyntaxFactory.Identifier("firstRender"))
                            .WithType(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.BoolKeyword))))))
                    ;
            }
            else
            {
                methodDeclaration = SyntaxFactory.MethodDeclaration(
                    returnType,
                    methodName)
                    .WithModifiers(
                        SyntaxFactory.TokenList(
                            SyntaxFactory.Token(SyntaxKind.ProtectedKeyword),
                            SyntaxFactory.Token(SyntaxKind.OverrideKeyword)));
            }

            if (methodName.Contains("Async"))
            {
                methodDeclaration = methodDeclaration.WithExpressionBody(
                    SyntaxFactory.ArrowExpressionClause(SyntaxFactory.ParseExpression("Task.CompletedTask")));
                methodDeclaration = methodDeclaration.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            }
            else
            {
                methodDeclaration = methodDeclaration.WithBody(SyntaxFactory.Block());
            }

            partialClassDeclaration = partialClassDeclaration.AddMembers(methodDeclaration);
        }

        return partialClassDeclaration;
    }

    private IEnumerable<string> GetLifecycleMethodNames()
    {
        return new string[]
        {
                "OnInitialized",
                "OnParametersSet",
                "OnAfterRender",
                "OnInitializedAsync",
                "OnParametersSetAsync",
                "OnAfterRenderAsync"
        };
    }

    private IEnumerable<StatementSyntax> GetParameterAttributeStatements(INamedTypeSymbol classSymbol)
    {
        List<StatementSyntax> statements = [];

        INamedTypeSymbol? baseClass = classSymbol.BaseType;
        int attributeOrder = 0;

        while (baseClass != null && !baseClass.Name.Equals("Object"))
        {
            statements.AddRange(GetParameterAttributeStatementsFromClass(baseClass, ref attributeOrder));
            baseClass = baseClass.BaseType;
        }

        return statements;
    }

    private IEnumerable<StatementSyntax> GetParameterAttributeStatementsFromClass(
        INamedTypeSymbol classSymbol,
        ref int attributeOrder)
    {
        List<StatementSyntax> statements = [];

        // Get properties with [Parameter] attribute
        foreach (ISymbol member in classSymbol.GetMembers())
        {
            if (member is IPropertySymbol propertySymbol && HasParameterAttribute(propertySymbol))
            {
                statements.Add(CreateAddAttributeStatement(propertySymbol.Name, propertySymbol.Name, ref attributeOrder));
            }
        }

        return statements;
    }

    private bool HasParameterAttribute(IPropertySymbol propertySymbol) =>
        propertySymbol.GetAttributes().Any(attr =>
            attr.AttributeClass?.Name is "ParameterAttribute" or "Parameter");

    private StatementSyntax CreateAddAttributeStatement(string attributeName, string attributeValue, ref int attributeOrder)
    {
        return SyntaxFactory.ExpressionStatement(
            SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName("builder"),
                    SyntaxFactory.IdentifierName("AddAttribute")))
            .WithArgumentList(
                SyntaxFactory.ArgumentList(
                    SyntaxFactory.SeparatedList<ArgumentSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            SyntaxFactory.Argument(
                                SyntaxFactory.LiteralExpression(
                                    SyntaxKind.NumericLiteralExpression,
                                    SyntaxFactory.Literal(attributeOrder++))),
                            SyntaxFactory.Token(SyntaxKind.CommaToken),
                            SyntaxFactory.Argument(
                                SyntaxFactory.LiteralExpression(
                                    SyntaxKind.StringLiteralExpression,
                                    SyntaxFactory.Literal(attributeName))),
                            SyntaxFactory.Token(SyntaxKind.CommaToken),
                            SyntaxFactory.Argument(
                                SyntaxFactory.IdentifierName(attributeValue))
                        }))));
    }
}