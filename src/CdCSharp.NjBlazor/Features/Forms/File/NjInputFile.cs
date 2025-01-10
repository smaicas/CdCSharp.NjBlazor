using CdCSharp.NjBlazor.Core.SourceGenerators.Abstractions;

namespace CdCSharp.NjBlazor.Features.Forms.File;

/// <summary>
/// Represents an input file control that extends the functionality of the base input file control.
/// De-multiplexer for NjInputFileVariant
/// </summary>
[ComponentDeMux<NjInputFileVariant>]
public partial class NjInputFile : NjInputFileBase
{
}