using CdCSharp.SequentialGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

public class ComponentDeMuxGenerator : ISequentialGenerator
{
    private static readonly string[] AttributeName = { "ComponentDeMux", "ComponentDeMuxAttribute" };

    public string Name => nameof(ComponentDeMuxGenerator);

    public IncrementalValuesProvider<INamedTypeSymbol> ConfigureProvider(IncrementalGeneratorInitializationContext context)
    {
        return context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsClassWithComponentDeMuxAttribute(s),
                transform: static (ctx, _) => GetSemanticTarget(ctx))
            .Where(static m => m is not null)!;
    }

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

    private static bool IsComponentDeMuxAttribute(AttributeSyntax attributeSyntax)
    {
        bool isGeneric = attributeSyntax.Name is GenericNameSyntax;
        if (isGeneric)
        {
            return AttributeName.Any(expected => attributeSyntax.Name.ToString().Contains(expected));
        }
        return false;
    }

    private static INamedTypeSymbol? GetSemanticTarget(GeneratorSyntaxContext context)
    {
        ClassDeclarationSyntax classDeclaration = (ClassDeclarationSyntax)context.Node;
        SemanticModel model = context.SemanticModel;
        INamedTypeSymbol? classSymbol = model.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;

        if (classSymbol == null)
            return null;

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

    public void Execute(SequentialGeneratorExecutionContext context)
    {
        if (context.Classes.IsDefaultOrEmpty)
            return;

        List<(string ClassName, List<INamedTypeSymbol> ClassGroup)> classGroups = GroupPartialClasses(context.Classes);
        foreach ((string className, List<INamedTypeSymbol> classGroup) in classGroups)
        {
            ProcessClassGroup(context, className, classGroup);
        }
    }

    private List<(string ClassName, List<INamedTypeSymbol> ClassGroup)> GroupPartialClasses(ImmutableArray<INamedTypeSymbol> classes)
    {
        List<(string ClassName, List<INamedTypeSymbol> ClassGroup)> groups = [];

        foreach (INamedTypeSymbol? classSymbol in classes.Distinct(SymbolEqualityComparer.Default))
        {
            if (!groups.Any(c => c.ClassName == classSymbol.Name))
            {
                groups.Add((classSymbol.Name, []));
            }
            groups.First(c => c.ClassName == classSymbol.Name).ClassGroup.Add(classSymbol);
        }

        return groups;
    }

    private void ProcessClassGroup(
        SequentialGeneratorExecutionContext context,
        string className,
        List<INamedTypeSymbol> classGroup)
    {
        List<ClassDeclarationSyntax> allClassDeclarations = GetClassDeclarations(classGroup);
        if (!allClassDeclarations.Any())
            return;

        ClassDeclarationSyntax primaryDeclaration = allClassDeclarations.First();
        string namespaceName = GetNamespaceName(primaryDeclaration);
        INamedTypeSymbol combinedClassSymbol = classGroup.First();

        string mergedPartialClassCode = GeneratePartialClassCode(
            context,
            primaryDeclaration,
            namespaceName,
            combinedClassSymbol,
            allClassDeclarations);

        context.AddSource(
            $"{className}.CDM.g.cs",
            SourceText.From(mergedPartialClassCode, Encoding.UTF8));
    }

    private List<ClassDeclarationSyntax> GetClassDeclarations(List<INamedTypeSymbol> classGroup)
    {
        return classGroup
            .SelectMany(symbol => symbol.DeclaringSyntaxReferences)
            .Select(reference => reference.GetSyntax())
            .OfType<ClassDeclarationSyntax>()
            .ToList();
    }

    private string GetNamespaceName(ClassDeclarationSyntax classDeclaration)
    {
        NamespaceDeclarationSyntax? namespaceDeclaration = classDeclaration.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        FileScopedNamespaceDeclarationSyntax? fileScopedNamespaceDeclaration = classDeclaration.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();

        if (namespaceDeclaration != null)
            return namespaceDeclaration.Name.ToString();
        else if (fileScopedNamespaceDeclaration != null)
            return fileScopedNamespaceDeclaration.Name.ToString();
        else
            return string.Empty;
    }

    private string GeneratePartialClassCode(
        SequentialGeneratorExecutionContext context,
        ClassDeclarationSyntax primaryClassDeclaration,
        string namespaceName,
        INamedTypeSymbol classSymbol,
        List<ClassDeclarationSyntax> allPartialDeclarations)
    {
        SemanticModel semanticModel = context.Compilation.GetSemanticModel(primaryClassDeclaration.SyntaxTree);

        string className = primaryClassDeclaration.Identifier.Text;
        TypeParameterListSyntax? typeParameterList = primaryClassDeclaration.TypeParameterList;

        ClassDeclarationSyntax partialClassDeclaration = SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
            .WithBaseList(primaryClassDeclaration.BaseList)
            .WithTypeParameterList(typeParameterList);

        // Generate code considering all partial declarations
        partialClassDeclaration = AddVariantEnumDependentCode(context.SourceContext, semanticModel, primaryClassDeclaration, partialClassDeclaration);
        partialClassDeclaration = AddLifeCycleMethodOverrides(partialClassDeclaration);

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
            throw new ArgumentException("Attribute not found.");

        IdentifierNameSyntax? enumTypeIdentifier = attribute.DescendantNodes()
            .OfType<TypeArgumentListSyntax>()
            .FirstOrDefault()?.Arguments.FirstOrDefault() as IdentifierNameSyntax;

        if (enumTypeIdentifier == null)
            throw new ArgumentException("Enum type identifier not found.");

        SymbolInfo enumSymbol = semanticModel.GetSymbolInfo(enumTypeIdentifier);

        if (enumSymbol.Symbol is not ITypeSymbol typeSymbol)
            throw new ArgumentException("Enum type symbol not found.");

        EnumDeclarationSyntax? enumDeclaration = FindEnumDeclaration(semanticModel.Compilation, typeSymbol);

        if (enumDeclaration == null)
            throw new ArgumentException("Enum declaration not found.");

        IEnumerable<string> enumNames = enumDeclaration.Members.Select(m => m.Identifier.Text);
        string namespaceName = enumSymbol.Symbol.ContainingNamespace.ToString();

        partialClassDeclaration = AddVariantParameter(partialClassDeclaration, enumTypeIdentifier, enumNames, namespaceName);
        partialClassDeclaration = AddBuildRenderTreeMethod(semanticModel, originalClassDeclaration, partialClassDeclaration, enumTypeIdentifier, enumNames, namespaceName);

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

    private ClassDeclarationSyntax AddBuildRenderTreeMethod(
        SemanticModel semanticModel,
        ClassDeclarationSyntax originalClassDeclaration,
        ClassDeclarationSyntax partialClassDeclaration,
        IdentifierNameSyntax enumTypeIdentifier,
        IEnumerable<string> enumNames,
        string namespaceName)
    {
        List<SwitchSectionSyntax> switchSections = [];

        foreach (string name in enumNames)
        {
            CaseSwitchLabelSyntax caseSwitchLabel = SyntaxFactory.CaseSwitchLabel(
                SyntaxFactory.ParseExpression($"{namespaceName}.{enumTypeIdentifier.Identifier.Text}.{name}")
            );

            TypeParameterListSyntax? typeParameterList = originalClassDeclaration.TypeParameterList;
            string identifierString;
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

    private ClassDeclarationSyntax AddVariantParameter(
        ClassDeclarationSyntax partialClassDeclaration,
        IdentifierNameSyntax enumTypeIdentifier,
        IEnumerable<string> enumNames,
        string namespaceName)
    {
        QualifiedNameSyntax qualifiedTypeName = SyntaxFactory.QualifiedName(
            SyntaxFactory.IdentifierName(namespaceName),
            SyntaxFactory.IdentifierName(enumTypeIdentifier.Identifier.Text)
        );

        PropertyDeclarationSyntax variantProperty = SyntaxFactory.PropertyDeclaration(
                qualifiedTypeName, "Variant")
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
                    SyntaxFactory.ParseExpression($"{namespaceName}.{enumTypeIdentifier.Identifier.Text}.{enumNames.First()}")))
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

        partialClassDeclaration = partialClassDeclaration.AddMembers(variantProperty);
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
}