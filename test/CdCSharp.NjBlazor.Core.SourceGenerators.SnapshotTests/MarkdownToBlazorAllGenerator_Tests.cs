using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;

public class MarkdownToBlazorAllGenerator_Tests
{
    [Fact]
    public async Task WhenClassWithAttribute_ShouldGenerateColorsClass()
    {
        // Source code
        string source = @"
using CdCSharp.NjBlazor.SourceGenerators.Abstractions;
namespace TestApp;
[MarkdownResourcesAll]
public class AllDocs
{
}
";
        // Additional file content
        string markdownContent = @"# Test Header
This is a test markdown file.
## Section
Some content here.";

        // Create test additional file
        TestAdditionalText[] additionalFiles = new[]
        {
            new TestAdditionalText(
                path: "/TestFiles/test.md",
                content: markdownContent)
        };

        // Create the compilation
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        List<PortableExecutableReference> references = GetDefaultReferences();
        CSharpCompilation compilation = CSharpCompilation.Create(
            "TestApp",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Create and run the generator with additional files
        MarkdownToBlazorAllGenerator generator = new();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: new[] { generator }.Select(GeneratorExtensions.AsSourceGenerator),
            additionalTexts: additionalFiles,
            parseOptions: (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
            optionsProvider: null);

        driver = driver.RunGenerators(compilation);

        // Validate results
        ValidateGeneratorOutput(driver.GetRunResult());

        // Verify the generated output
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

// Helper class to create test additional files
public class TestAdditionalText : AdditionalText
{
    private readonly string _text;
    public override string Path { get; }

    public TestAdditionalText(string path, string content)
    {
        Path = path;
        _text = content;
    }

    public override SourceText GetText(CancellationToken cancellationToken = default) => SourceText.From(_text);
}