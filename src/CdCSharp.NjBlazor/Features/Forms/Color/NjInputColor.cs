using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Color;

/// <summary>
/// Represents a custom input control for selecting a color. De-multiplexer for NjInputColorVariant
/// </summary>
[ComponentDeMux<NjInputColorVariant>]
public partial class NjInputColor : NjInputColorBase
{
}