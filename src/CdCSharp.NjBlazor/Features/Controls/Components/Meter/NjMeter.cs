using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Meter;

/// <summary>
/// Represents a custom meter control that extends the functionality of the base NjMeterBase class.
/// De-multiplexer for NjMeterVariant
/// </summary>
[ComponentDeMux<NjMeterVariant>]
public partial class NjMeter : NjMeterBase
{
}