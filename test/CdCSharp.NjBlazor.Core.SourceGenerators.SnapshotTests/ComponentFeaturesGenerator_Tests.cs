using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CdCSharp.NjBlazor.Core.SourceGenerators.SnapshotTests;
public class ComponentFeaturesGenerator_Tests
{
    [Fact]
    public async Task WithAttr_ShouldGeneratePartialFeatureClass()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

public class ActivableComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class ActivableComponent : NjComponentBase
{
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithAttrMultiple_ShouldGenerateAll()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

public class ActivableComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

public class DisabledComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string DisabledClass { get; set; } = CssClassReferences.Disabled;
}

[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature), typeof(DisabledComponentFeature))]
public partial class ActivableComponent : NjComponentBase
{
}";
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithNoAttr_ShouldNotGenerateAnyCode()
    {
        string source = @"
namespace CdCSharp.NjBlazor.Core.Components;

public class SimpleComponent
{
}
";
        // Validar que no se genera código
        await RunAndVerifyGenerator(source, expectGeneratedCode: false);
    }

    [Fact]
    public async Task WithAttrInheritClass_ShouldRespectInheritance()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

// Clase base con herencia
public abstract class CustomBaseComponent : NjComponentBase
{
    [Parameter]
    public string CommonParameter { get; set; }
}

// Clase con funcionalidad activable
public class ActivableComponentFeature : CustomBaseComponent
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

// Clase anotada con herencia explícita
[ComponentFeatures<CustomBaseComponent>(typeof(ActivableComponentFeature))]
public partial class AdvancedActivableComponent : CustomBaseComponent
{
    [Parameter]
    public string ExtraParameter { get; set; }
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithAttrInheritClass_ShouldRespectInheritance2()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

// Clase base con herencia
public abstract class CustomBaseComponent : NjComponentBase
{
    [Parameter]
    public string CommonParameter { get; set; }
}

// Clase con funcionalidad activable
public class ActivableComponentFeature : CustomBaseComponent
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

// Clase anotada con herencia explícita
[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class AdvancedActivableComponent : CustomBaseComponent
{
    [Parameter]
    public string ExtraParameter { get; set; }
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithAttrInheritClass_ShouldRespectInheritance3()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

// Clase base con herencia
public abstract class CustomBaseComponent : NjComponentBase
{
    [Parameter]
    public string CommonParameter { get; set; }
}

// Clase con funcionalidad activable
public class ActivableComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

// Clase anotada con herencia explícita
[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class AdvancedActivableComponent : CustomBaseComponent
{
    [Parameter]
    public string ExtraParameter { get; set; }
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithGenerics_ShouldRespectInheritance()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

// Clase base con herencia
public abstract class CustomBaseComponent<T> : NjComponentBase
{
    [Parameter]
    public string CommonParameter { get; set; }
}

// Clase con funcionalidad activable
public class ActivableComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

// Clase anotada con herencia explícita
[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class AdvancedActivableComponent : CustomBaseComponent<string>
{
    [Parameter]
    public string ExtraParameter { get; set; }
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithGenerics_ShouldRespectInheritance2()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

// Clase base con herencia
public abstract class CustomBaseComponent<T> : NjComponentBase
{
    [Parameter]
    public string CommonParameter { get; set; }
}

// Clase con funcionalidad activable
public class ActivableComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

// Clase anotada con herencia explícita
[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class AdvancedActivableComponent : CustomBaseComponent<NjComponentBase>
{
    [Parameter]
    public string ExtraParameter { get; set; }
}
";

        // Validar código generado
        await RunAndVerifyGenerator(source);
    }

    [Fact]
    public async Task WithGenerics_ShouldRespectInheritance3()
    {
        string source = @"
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;

// Clase base con herencia
public abstract class CustomBaseComponent<T> : NjComponentBase
{
    [Parameter]
    public string CommonParameter { get; set; }
}

// Clase con funcionalidad activable
public class ActivableComponentFeature : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}

// Clase anotada con herencia explícita
[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class AdvancedActivableComponent<TValue> : CustomBaseComponent<TValue>
{
    [Parameter]
    public string ExtraParameter { get; set; }
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