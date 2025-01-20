using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Abstractions.Css;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Containers.Components;

/// <summary>
/// Represents a border box component that extends the functionality of the base component class.
/// </summary>
public partial class NjBorderBox : NjComponentBase
{
    /// <summary>
    /// Gets or sets the border radius.
    /// </summary>
    /// <value>
    /// The border radius value.
    /// </value>
    [Parameter]
    public int BorderRadius { get; set; } = 5;

    /// <summary>
    /// Gets or sets the border width.
    /// </summary>
    /// <value>
    /// The border width value.
    /// </value>
    [Parameter]
    public int BorderWidth { get; set; } = 2;

    /// <summary>
    /// Gets or sets the border style.
    /// </summary>
    /// <value>
    /// The border radius value.
    /// </value>
    [Parameter]
    public BorderStyleMode BorderStyle { get; set; } = BorderStyleMode.Solid;

    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the color in CSS format.
    /// </summary>
    /// <value>
    /// The CSS color value.
    /// </value>
    [Parameter]
    public CssColor? Color { get; set; }

    private string GetBoxShadow() => CssTools.CalculateCssBorderValue(BorderStyle, BorderWidth, BorderRadius, Color);
}