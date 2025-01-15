using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.Number;

/// <summary>
/// Represents a custom input number control that extends the functionality of the base
/// NjInputNumberBase class. De-multiplexer for NjInputNumberVariant
/// </summary>
[ComponentDeMux<NjInputNumberVariant>]
public partial class NjInputNumber : NjInputNumberBase
{
}