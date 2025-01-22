using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;

public class ComponentDeMuxGenerator_Tests
{
    [Fact]
    public async void GeneratedCodeMatchesSnapshot()
    {
        string source = @"namespace Nj.Blazor;

[ComponentDeMux<NjInputTextVariant>]
public partial class NjInputText : NjInputTextBase
{
    [Parameter] public bool ItWontBeParameter { get; set; }
}

public enum NjInputTextVariant
{
    Flat,
    Filled,
    Outline
}

public abstract class NjInputTextBase {
    [Parameter] public bool IsTextArea { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get;set; }
}
";

        // Crear la compilación
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        List<PortableExecutableReference> references = GetDefaultReferences();
        CSharpCompilation compilation = CSharpCompilation.Create(
            "TestApp",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Ejecutar el generador
        ComponentGenerator generator = new();
        CSharpGeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = (CSharpGeneratorDriver)driver.RunGenerators(compilation);

        // Validar resultados
        ValidateGeneratorOutput(driver.GetRunResult());

        // Verificar la salida generada
        await Verify(driver);
    }

    [Fact]
    public async void ShouldGenerateWhenCodeInDifferentFiles()
    {
        string enumSource = @"namespace Nj.Blazor;

    public enum NjInputTextVariant
    {
        Flat,
        Filled,
        Outline
    }
";
        string source = @"namespace Nj.Blazor;

[ComponentDeMux<NjInputTextVariant>]
public partial class NjInputText : NjInputTextBase
{
    [Parameter] public bool ItWontBeParameter { get; set; }
}

public abstract class NjInputTextBase {
    [Parameter] public bool IsTextArea { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get;set; }
}
";

        // Crear la compilación
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        SyntaxTree additionalSyntaxTree = CSharpSyntaxTree.ParseText(enumSource);

        List<PortableExecutableReference> references = GetDefaultReferences();
        CSharpCompilation compilation = CSharpCompilation.Create(
            "TestApp",
            new[] { syntaxTree, additionalSyntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Ejecutar el generador
        ComponentGenerator generator = new();
        CSharpGeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = (CSharpGeneratorDriver)driver.RunGenerators(compilation);

        // Validar resultados
        ValidateGeneratorOutput(driver.GetRunResult());

        // Verificar la salida generada
        await Verify(driver);
    }

    private static List<PortableExecutableReference> GetDefaultReferences()
    {
        List<PortableExecutableReference> projectReferences = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .ToList();

        System.Reflection.Assembly annotationAssembly = typeof(ComponentDeMuxAttribute<>).Assembly;

        if (!projectReferences.Any(r => string.Equals(r.Display, annotationAssembly.Location, StringComparison.OrdinalIgnoreCase)))
        {
            PortableExecutableReference specificReference = MetadataReference.CreateFromFile(annotationAssembly.Location);
            projectReferences.Add(specificReference);
        }

        return projectReferences;
    }

    private static void ValidateGeneratorOutput(GeneratorDriverRunResult runResult)
    {
        System.Collections.Immutable.ImmutableArray<Diagnostic> diagnostics = runResult.Diagnostics;
        if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
        {
            string errors = string.Join("\n", diagnostics.Select(d => d.ToString()));
            throw new InvalidOperationException($"Se produjeron errores durante la generación: \n{errors}");
        }

        if (!runResult.GeneratedTrees.Any())
        {
            throw new InvalidOperationException("El generador no produjo ningún código.");
        }
    }
}