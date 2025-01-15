using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Date;

/// <summary> Represents an input control for selecting a date value of type TValue. Inherits from
/// NjInputDateBase<TValue>. De-multiplexer for NjInputDateVariant </summary>
[ComponentDeMux<NjInputDateVariant>]
public partial class NjInputDate<TValue> : NjInputDateBase<TValue>
{
}