using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Media.Components;

/// <summary>
/// Represents an image component in the Nj framework. Inherits functionality from the
/// NjComponentBase class.
/// </summary>
public partial class NjImage : NjComponentBase
{
    /// <summary>
    /// Gets or sets the alignment mode for the element along the cross axis.
    /// </summary>
    /// <value>
    /// The alignment mode for the element along the cross axis.
    /// </value>
    [Parameter]
    public AlignSelfMode AlignSelf { get; set; } = AlignSelfMode.Center;

    /// <summary>
    /// Gets or sets the alternative text.
    /// </summary>
    /// <value>
    /// The alternative text.
    /// </value>
    [Parameter]
    public string Alt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the height value.
    /// </summary>
    /// <value>
    /// The height value.
    /// </value>
    [Parameter]
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the source of an element.
    /// </summary>
    /// <value>
    /// The source of the element.
    /// </value>
    [Parameter]
    public string Src { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the width value.
    /// </summary>
    /// <value>
    /// The width value.
    /// </value>
    [Parameter]
    public int Width { get; set; }

    private string CssClass =>
        AlignSelf switch
        {
            AlignSelfMode.Auto => CssClassReferences.AlignSelf.AlignSelfAuto,
            AlignSelfMode.FlexStart => CssClassReferences.AlignSelf.AlignSelfStart,
            AlignSelfMode.FlexEnd => CssClassReferences.AlignSelf.AlignSelfEnd,
            AlignSelfMode.Center => CssClassReferences.AlignSelf.AlignSelfCenter,
            AlignSelfMode.Baseline => CssClassReferences.AlignSelf.AlignSelfBaseline,
            AlignSelfMode.Stretch => CssClassReferences.AlignSelf.AlignSelfStretch,
            _ => throw new ArgumentOutOfRangeException()
        };
}