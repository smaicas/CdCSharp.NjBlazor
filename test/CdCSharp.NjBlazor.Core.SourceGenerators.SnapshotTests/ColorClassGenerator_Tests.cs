using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;

public class ColorClassGenerator_Tests
{
    [Fact]
    public async Task WhenClassWithAttribute_ShouldGenerateColorsClass()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Core.Css;

[AutogenerateCssColors]
public static partial class NjColors
{
}";

        // Crear la compilación
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        List<PortableExecutableReference> references = GetDefaultReferences();
        CSharpCompilation compilation = CSharpCompilation.Create(
            "TestApp",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Ejecutar el generador
        ColorClassGenerator generator = new();
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

        System.Reflection.Assembly annotationAssembly = typeof(AutogenerateCssColorsAttribute).Assembly;

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