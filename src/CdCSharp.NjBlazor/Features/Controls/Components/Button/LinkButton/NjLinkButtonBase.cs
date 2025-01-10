using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.LinkButton;

/// <summary>
/// Base class for a link button component in the NjControl library.
/// </summary>
public abstract class NjLinkButtonBase : NjControlComponentBase
{
    private string _text = string.Empty;

    /// <summary>Gets or sets the background color in CSS format.</summary>
    /// <value>The background color in CSS format.</value>
    [Parameter]
    public CssColor? BackgroundColor { get; set; }

    /// <summary>Gets or sets the color value in CSS format.</summary>
    /// <value>The color value in CSS format.</value>
    [Parameter]
    public CssColor? Color { get; set; }

    /// <summary>Gets or sets the post-adornment string.</summary>
    /// <value>The post-adornment string.</value>
    [Parameter]
    public string? PostAdornment { get; set; }

    /// <summary>Gets or sets the color for the post-adornment.</summary>
    /// <value>The color for the post-adornment.</value>
    [Parameter]
    public CssColor? PostAdornmentColor { get; set; }

    /// <summary>Gets or sets the string to be displayed before the main content.</summary>
    /// <value>The string to be displayed before the main content.</value>
    [Parameter]
    public string? PreAdornment { get; set; }

    /// <summary>Gets or sets the color of the pre-adornment.</summary>
    /// <value>The color of the pre-adornment.</value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>Gets or sets the padding value.</summary>
    /// <value>The padding value.</value>
    [Parameter]
    public int Padding { get; set; }

    /// <summary>Gets or sets the text value with a specified transformation.</summary>
    /// <value>The text value.</value>
    /// <remarks>This property applies the specified transformation to the text value when getting or setting it.</remarks>
    [Parameter]
    public string Text
    {
        get => TextTransform.Invoke(_text);
        set => _text = value;
    }

    /// <summary>Gets or sets the href attribute value.</summary>
    /// <value>The href attribute value.</value>
    [Parameter]
    public string Href { get; set; } = "#";

    /// <summary>Gets or sets the target for the link button.</summary>
    /// <value>The target for the link button.</value>
    [Parameter]
    public NjLinkButtonTarget Target { get; set; } = NjLinkButtonTarget.Self;

    /// <summary>Gets or sets the function used to transform text.</summary>
    /// <value>The function that transforms the input string.</value>
    /// <remarks>The default transformation function converts the input string to uppercase.</remarks>
    [Parameter]
    public Func<string, string> TextTransform { get; set; } = (s) => s.ToUpper();

    /// <summary>Gets the target attribute value for a link based on the specified target type.</summary>
    /// <value>The target attribute value for the link.</value>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the target type is not recognized.</exception>
    protected string LinkTarget =>
        Target switch
        {
            NjLinkButtonTarget.Self => "_self",
            NjLinkButtonTarget.Blank => "_blank",
            NjLinkButtonTarget.Parent => "_parent",
            NjLinkButtonTarget.Top => "_top",
            _ => throw new ArgumentOutOfRangeException()
        };

    /// <summary>Gets the CSS class based on the focus state.</summary>
    /// <value>The CSS class if focused; otherwise, an empty string.</value>
    protected string FocusClass => IsFocused ? CssClassReferences.Focus : string.Empty;

    /// <summary>Gets the inline style for the element.</summary>
    /// <value>A string representing the inline style of the element.</value>
    protected string InlineStyle
    {
        get
        {
            StringBuilder sb = new();
            if (BackgroundColor != null)
                sb.Append(
                    $"background-color: {BackgroundColor.ToString(ColorOutputFormats.Rgba)};"
                );
            if (Color != null)
                sb.Append($"color: {Color.ToString(ColorOutputFormats.Rgba)};");
            return sb.ToString();
        }
    }

    private bool IsFocused { get; set; }

    /// <summary>
    /// Handles the focus event asynchronously.
    /// </summary>
    /// <param name="focusEventArgs">The focus event arguments.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnFocusAsync(FocusEventArgs? focusEventArgs)
    {
        IsFocused = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the asynchronous event when focus moves out from the element.
    /// </summary>
    /// <param name="focusEventArgs">The event arguments related to the focus change.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task OnFocusOutAsync(FocusEventArgs? focusEventArgs)
    {
        IsFocused = false;
        return Task.CompletedTask;
    }
}
