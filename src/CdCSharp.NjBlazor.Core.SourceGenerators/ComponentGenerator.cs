using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

// Main generator implementation
[Generator]
public class ComponentGenerator : IIncrementalGenerator
{
    private readonly List<(string Name, IComponentCodeGenerator Generator)> _generators = [];

    public ComponentGenerator()
    {
        RegisterGenerator("Features", new ComponentFeaturesGenerator());
        RegisterGenerator("DeMux", new ComponentDeMuxGenerator());
    }

    public void RegisterGenerator(string name, IComponentCodeGenerator generator) =>
        _generators.Add((name, generator));

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //// Generate marker
        //context.RegisterSourceOutput(context.CompilationProvider, (spc, compilation) =>
        //    GenerateMarker(spc));

        // Configure providers
        List<(string Name, IncrementalValuesProvider<INamedTypeSymbol> Provider)> providersWithNames = _generators
            .Select(g => (g.Name, Provider: g.Generator.ConfigureProvider(context)))
            .ToList();

        // Combine providers
        IncrementalValueProvider<GeneratorCompilationContext> combinedProviders = CombineProviders(context.CompilationProvider, providersWithNames);

        // Register sequential execution
        context.RegisterSourceOutput(combinedProviders, ExecuteGeneratorsSequentially);
    }

    private IncrementalValueProvider<GeneratorCompilationContext> CombineProviders(
       IncrementalValueProvider<Compilation> compilationProvider,
       List<(string Name, IncrementalValuesProvider<INamedTypeSymbol> Provider)> providers)
    {
        IncrementalValueProvider<GeneratorCompilationContext> initialValue = compilationProvider.Select((c, _) =>
            new GeneratorCompilationContext(c));

        return providers.Aggregate(initialValue,
            (current, provider) => current.Combine(provider.Provider.Collect())
                .Select((tuple, _) => tuple.Left.AddClasses(provider.Name, tuple.Right)));
    }

    private void ExecuteGeneratorsSequentially(SourceProductionContext sourceContext, GeneratorCompilationContext context)
    {
        Compilation currentCompilation = context.Compilation;

        foreach ((string name, IComponentCodeGenerator generator) in _generators)
        {
            if (!context.Classes.TryGetValue(name, out ImmutableArray<INamedTypeSymbol> classes))
                continue;

            GeneratorExecutionContext executionContext = new(
                currentCompilation,
                classes,
                sourceContext);

            // Execute generator
            generator.Execute(executionContext);

            // Update compilation for next generator
            currentCompilation = executionContext.Compilation;
        }
    }

    private static void GenerateMarker(SourceProductionContext context)
    {
        context.AddSource("ComponentFeaturesMarker.g.cs", SourceText.From(@"
namespace ComponentFeaturesGenerated {
    public static class Marker {
        public const string IsGenerated = ""true"";
    }
}", Encoding.UTF8));
    }
}

public class GeneratorCompilationContext
{
    public Compilation Compilation { get; }
    public Dictionary<string, ImmutableArray<INamedTypeSymbol>> Classes { get; }

    public GeneratorCompilationContext(Compilation compilation)
    {
        Compilation = compilation;
        Classes = [];
    }

    public GeneratorCompilationContext AddClasses(string generatorName, ImmutableArray<INamedTypeSymbol> classes)
    {
        Classes[generatorName] = classes;
        return this;
    }
}

public class GeneratorExecutionContext
{
    public Compilation Compilation { get; private set; }
    public ImmutableArray<INamedTypeSymbol> Classes { get; }
    public SourceProductionContext SourceContext { get; }
    private readonly List<(string FileName, SourceText Source)> _generatedSources = [];

    public GeneratorExecutionContext(
        Compilation compilation,
        ImmutableArray<INamedTypeSymbol> classes,
        SourceProductionContext sourceContext)
    {
        Compilation = compilation;
        Classes = classes;
        SourceContext = sourceContext;
    }

    public void AddSource(string fileName, SourceText sourceText)
    {
        _generatedSources.Add((fileName, sourceText));

        // Add to compilation immediately
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceText, path: fileName);
        Compilation = Compilation.AddSyntaxTrees(syntaxTree);

        // Add to source context immediately
        SourceContext.AddSource(fileName, sourceText);
    }

    public void ReportDiagnostic(Diagnostic diagnostic) => SourceContext.ReportDiagnostic(diagnostic);

    public CancellationToken CancellationToken => SourceContext.CancellationToken;

    public IReadOnlyList<(string FileName, SourceText Source)> GeneratedSources => _generatedSources;
}

// Modified interface to use the new context
public interface IComponentCodeGenerator
{
    IncrementalValuesProvider<INamedTypeSymbol> ConfigureProvider(IncrementalGeneratorInitializationContext context);
    void Execute(GeneratorExecutionContext context);
}