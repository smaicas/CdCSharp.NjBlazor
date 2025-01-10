using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Radio;

/// <summary>
/// Represents a radio button input element.
/// Inherits properties and behavior from the base NjInputRadioBase class.
/// De-multiplexer for NjInputRadioVariant
/// </summary>
[ComponentDeMux<NjInputRadioVariant>]
public partial class NjInputRadio : NjInputRadioBase
{
}