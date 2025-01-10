using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Text;

/// <summary>
/// Represents a custom input text control that extends the functionality of the base input text control.
/// De-multiplexer for NjInputTextVariant
/// </summary>
[ComponentDeMux<NjInputTextVariant>]
public partial class NjInputText : NjInputTextBase
{
}