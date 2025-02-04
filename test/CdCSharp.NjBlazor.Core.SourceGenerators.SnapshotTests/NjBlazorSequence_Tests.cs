using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;

public class NjBlazorSequence_Tests
{
    [Fact]
    public async Task GeneratedCodeMatchesExpectations()
    {
        // Arrange - Source code with test generators
        string source = @"
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Text;

/// <summary>
/// Represents a custom input text control that extends the functionality of the base input text
/// control. De-multiplexer for NjInputTextVariant
/// </summary>
[ComponentDeMux<NjInputTextVariant>]
public partial class NjInputText : NjInputTextBase
{
}

/// <summary>
/// Variants for <see cref=""NjInputText"" />
/// </summary>
public enum NjInputTextVariant
{
    Flat,
    Filled,
    Outline
}

";

        // Act - Create and execute compilation with generator
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        List<MetadataReference> references = GetDefaultReferences();

        CSharpCompilation compilation = CSharpCompilation.Create(
            "TestApp",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // Create and run generator
        NjBlazorGeneratorsSequence generator = new();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out Compilation outputCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        // Assert - Validate results
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        // Verify generated content using Verify
        await Verify(driver);
    }

    private static List<MetadataReference> GetDefaultReferences()
    {
        List<MetadataReference> references = [];

        string[] trustedAssemblies = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);
        string[] requiredAssemblies = new[]
        {
            "System.Runtime",
            "System.Private.CoreLib",
            "Microsoft.CodeAnalysis",
            "Microsoft.CodeAnalysis.CSharp",
            "System.Collections",
            "System.Collections.Immutable",
        };

        foreach (string assembly in trustedAssemblies)
        {
            if (requiredAssemblies.Any(required =>
                Path.GetFileNameWithoutExtension(assembly).Equals(required, StringComparison.OrdinalIgnoreCase)))
            {
                references.Add(MetadataReference.CreateFromFile(assembly));
            }
        }

        // Add reference to assembly containing ISequentialGenerator
        System.Reflection.Assembly currentAssembly = typeof(ISequentialGenerator).Assembly;
        references.Add(MetadataReference.CreateFromFile(currentAssembly.Location));

        return references;
    }
}