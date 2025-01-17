using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.ActionButton;

/// <summary>
/// Base class for custom action button components.
/// </summary>
public abstract class NjActionButtonBase : NjControlComponentBase
{
    /// <summary>
    /// Gets or sets the background color in CSS format.
    /// </summary>
    /// <value>
    /// The background color in CSS format.
    /// </value>
    [Parameter]
    public CssColor? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the color value in CSS format.
    /// </summary>
    /// <value>
    /// The color value in CSS format.
    /// </value>
    [Parameter]
    public CssColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the icon associated with the component.
    /// </summary>
    /// <value>
    /// The icon path.
    /// </value>
    [Parameter]
    public string Icon { get; set; } = Media.Icons.NjIcons.Custom.Uncategorized.NoIcon;

    /// <summary>
    /// Gets or sets the event callback for handling mouse click events.
    /// </summary>
    /// <value>
    /// The event callback for handling mouse click events.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    public override Dictionary<string, string> GetInlineStyles()
    {
        Dictionary<string, string> styles = base.GetInlineStyles();

        if (BackgroundColor != null)
        {
            styles.TryAdd($"background-color", BackgroundColor.ToString(ColorOutputFormats.Rgba));
        }
        if (Color != null)
        {
            styles.TryAdd($"color", Color.ToString(ColorOutputFormats.Rgba));
        }
        return styles;
    }
}