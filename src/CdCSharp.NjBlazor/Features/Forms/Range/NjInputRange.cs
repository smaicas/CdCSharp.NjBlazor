using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Range;

/// <summary>
/// Represents a custom input range control that extends the functionality of the base input range control.
/// De-multiplexer for NjInputRangeVariant
/// </summary>
[ComponentDeMux<NjInputRangeVariant>]
public partial class NjInputRange : NjInputRangeBase
{
}