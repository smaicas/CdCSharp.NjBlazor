using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Containers.Components;

/// <summary>
/// Represents a shadow box component that extends the functionality of the base component class.
/// </summary>
public partial class NjShadowBox : NjComponentBase
{
    /// <summary>Gets or sets the X offset value.</summary>
    /// <value>The X offset value.</value>
    [Parameter]
    public int OffsetX { get; set; } = 3;

    /// <summary>Gets or sets the Y offset value.</summary>
    /// <value>The Y offset value.</value>
    [Parameter]
    public int OffsetY { get; set; } = 3;

    /// <summary>Gets or sets the blur radius for an effect.</summary>
    /// <value>The blur radius value.</value>
    [Parameter]
    public int BlurRadius { get; set; } = 5;

    /// <summary>Gets or sets the spread radius for a specific operation.</summary>
    /// <value>The spread radius value.</value>
    [Parameter]
    public int SpreadRadius { get; set; } = 5;

    /// <summary>Gets or sets the color in CSS format.</summary>
    /// <value>The CSS color value.</value>
    [Parameter]
    public CssColor Color { get; set; } = NjColors.Black.Default.SetAlpha(0.2);

    /// <summary>Gets or sets a value indicating whether the inset is applied.</summary>
    /// <value>True if the inset is applied; otherwise, false.</value>
    [Parameter]
    public bool Inset { get; set; } = false;

    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetBoxShadow()
    {
        string insetText = Inset ? "inset " : string.Empty;
        return $"{insetText}{OffsetX}px {OffsetY}px {BlurRadius}px {SpreadRadius}px {Color.ToString(ColorOutputFormats.Rgba)}";
    }
}
