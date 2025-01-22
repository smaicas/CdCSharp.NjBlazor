using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;
public class ComponentGenerator_Tests
{
    [Fact]
    public async Task WithAttrs_ShouldGenerateAll()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Abstractions.Components.Features;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

public partial class ActivableComponentFeature :
{
    private bool _active;

    /// <summary>
    /// Gets or sets the activity status.
    /// </summary>
    /// <value>
    /// True if active, false if not.
    /// </value>
    /// <remarks>
    /// Setting the value triggers the ActiveChanged event.
    /// </remarks>
    [Parameter]
    public bool Active
    {
        get => _active;
        set
        {
            if (_active == value)
                return;

            _active = value;
            ActiveChanged.InvokeAsync(_active);
        }
    }

    /// <summary>
    /// Gets or sets the event callback for when the active state changes.
    /// </summary>
    /// <value>
    /// The event callback for when the active state changes.
    /// </value>
    [Parameter]
    public EventCallback<bool> ActiveChanged { get; set; }

    /// <summary>
    /// Gets the CSS class for the active state.
    /// </summary>
    /// <value>
    /// The CSS class for the active state if active; otherwise, an empty string.
    /// </value>
    public virtual string ActiveClass => Active ? CssClassReferences.Active : string.Empty;

    private void ToggleActive() => Active = !Active;
}

[ComponentFeatures(typeof(ActivableComponentFeature))]
public abstract partial class NjActivableTextButtonBase : NjTextButtonBase
{
    protected virtual async Task ProcessClickAsync(MouseEventArgs? mouseEventArgs)
    {
        await OnClick.InvokeAsync(mouseEventArgs);
        ToggleActive();
    }

}

/// <summary>
/// Represents a custom activable text button control that extends the functionality of the base
/// activable text button. De-multiplexer for NjActivableTextButtonVariant
/// </summary>
[ComponentDeMux<NjActivableTextButtonVariant>]
public partial class NjActivableTextButton : NjActivableTextButtonBase
{
}
/// <summary>
/// Represents the variants available for an activable text button.
/// </summary>
public enum NjActivableTextButtonVariant
{
    UnderLine,
    TopLine,
    LeftLine,
    RightLine,
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    private static List<PortableExecutableReference> GetDefaultReferences()
    {
        List<PortableExecutableReference> projectReferences = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .ToList();

        System.Reflection.Assembly annotationAssembly = typeof(ComponentFeaturesAttribute).Assembly;

        if (!projectReferences.Any(r => string.Equals(r.Display, annotationAssembly.Location, StringComparison.OrdinalIgnoreCase)))
        {
            PortableExecutableReference specificReference = MetadataReference.CreateFromFile(annotationAssembly.Location);
            projectReferences.Add(specificReference);
        }

        return projectReferences;
    }

    private static async Task RunAndVerifyGenerator(string source, bool expectGeneratedCode = true)
    {
        // Crear compilación
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

        // Validar salida
        ValidateGeneratorOutput(driver.GetRunResult(), expectGeneratedCode);

        // Verificar instantáneas generadas
        await Verify(driver);
    }

    private static void ValidateGeneratorOutput(GeneratorDriverRunResult runResult, bool expectGeneratedCode)
    {
        System.Collections.Immutable.ImmutableArray<Diagnostic> diagnostics = runResult.Diagnostics;
        if (diagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
        {
            string errors = string.Join("\n", diagnostics.Select(d => d.ToString()));
            throw new InvalidOperationException($"Se produjeron errores durante la generación: \n{errors}");
        }

        if (expectGeneratedCode && !runResult.GeneratedTrees.Any())
        {
            throw new InvalidOperationException("El generador no produjo ningún código.");
        }

        if (!expectGeneratedCode && runResult.GeneratedTrees.Any())
        {
            throw new InvalidOperationException("Se generó código inesperadamente.");
        }
    }
}