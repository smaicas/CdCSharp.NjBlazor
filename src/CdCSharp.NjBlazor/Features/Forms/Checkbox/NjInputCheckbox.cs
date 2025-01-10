using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Checkbox;

/// <summary>
/// Represents a custom input checkbox control that extends the functionality of the base input checkbox.
/// De-multiplexer for NjInputCheckboxVariant
/// </summary>
[ComponentDeMux<NjInputCheckboxVariant>]
public partial class NjInputCheckbox : NjInputCheckboxBase
{
}

