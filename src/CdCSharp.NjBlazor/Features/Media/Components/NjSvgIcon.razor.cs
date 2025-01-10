using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CdCSharp.NjBlazor.Features.Media.Components;

/// <summary>
/// Represents an SVG icon component that inherits from the NjComponentBase class.
/// </summary>
public partial class NjSvgIcon : NjComponentBase
{
    /// <summary>Gets or sets the icon associated with the item.</summary>
    /// <value>The icon path.</value>
    /// <remarks>This property is decorated with the Parameter attribute and EditorRequired attribute.</remarks>
    [Parameter]
    [EditorRequired]
    public string Icon { get; set; } = Media.Icons.NjIcons.Custom.Uncategorized.NoIcon;

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the viewBox attribute value for SVG element.
    /// </summary>
    /// <value>The viewBox attribute value. Default is "0 0 24 24".</value>
    [Parameter]
    public string ViewBox { get; set; } = "0 0 24 24";

    /// <summary>
    /// Gets or sets the color in CSS format.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color.
    /// </value>
    [Parameter]
    public CssColor? Color { get; set; }

    /// <summary>Gets or sets the size of the SVG icon.</summary>
    /// <value>The size of the SVG icon.</value>
    [Parameter]
    public NjSvgIconSize Size { get; set; } = NjSvgIconSize.Medium;

    /// <summary>Gets or sets the event callback for mouse click events.</summary>
    /// <value>The event callback for mouse click events.</value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }
    private string IconSizeClass =>
        Size switch
        {
            NjSvgIconSize.Small => CssClassReferences.Icon.IconSizeSmall,
            NjSvgIconSize.Large => CssClassReferences.Icon.IconSizeLarge,
            NjSvgIconSize.XLarge => CssClassReferences.Icon.IconSizeLarge,
            NjSvgIconSize.XXLarge => CssClassReferences.Icon.IconSizeLarge,
            _ => CssClassReferences.Icon.IconSizeMedium,
        };
    private string InlineStyle =>
        Color == null ? string.Empty : $"color:{Color.ToString(ColorOutputFormats.Rgba)};";

    private bool IsSvg(string value) => value.StartsWith("<");
}
