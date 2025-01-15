using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.TextButton;

/// <summary>
/// Base class for a text button component in the NjControl library.
/// </summary>
public abstract class NjTextButtonBase : NjControlComponentBase
{
    private string _text = string.Empty;

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
    /// Gets or sets the event callback for mouse click events.
    /// </summary>
    /// <value>
    /// The event callback for mouse click events.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets the padding value.
    /// </summary>
    /// <value>
    /// The padding value.
    /// </value>
    [Parameter]
    public int Padding { get; set; }

    /// <summary>
    /// Gets or sets the post-adornment string.
    /// </summary>
    /// <value>
    /// The post-adornment string.
    /// </value>
    [Parameter]
    public string? PostAdornment { get; set; }

    /// <summary>
    /// Gets or sets the color of the post adornment.
    /// </summary>
    /// <value>
    /// The color of the post adornment.
    /// </value>
    [Parameter]
    public CssColor? PostAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets the string to be displayed before the main content.
    /// </summary>
    /// <value>
    /// The string to be displayed before the main content.
    /// </value>
    [Parameter]
    public string? PreAdornment { get; set; }

    /// <summary>
    /// Gets or sets the color of the pre-adornment.
    /// </summary>
    /// <value>
    /// The color of the pre-adornment.
    /// </value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets the text value with a specified transformation.
    /// </summary>
    /// <value>
    /// The text value.
    /// </value>
    /// <remarks>
    /// This property applies the specified transformation to the text value when getting or setting it.
    /// </remarks>
    [Parameter]
    public string Text
    {
        get => TextTransform.Invoke(_text);
        set => _text = value;
    }

    /// <summary>
    /// Gets or sets the function used to transform text.
    /// </summary>
    /// <value>
    /// The function that transforms text.
    /// </value>
    /// <remarks>
    /// The default transformation function converts text to uppercase.
    /// </remarks>
    [Parameter]
    public Func<string, string> TextTransform { get; set; } = (s) => s.ToUpper();

    /// <summary>
    /// Gets the CSS class for focusing based on the focus state.
    /// </summary>
    /// <value>
    /// The CSS class for focusing if the element is focused; otherwise, an empty string.
    /// </value>
    protected string FocusClass => IsFocused ? CssClassReferences.Focus : string.Empty;

    /// <summary>
    /// Gets the inline style for the element.
    /// </summary>
    /// <value>
    /// The inline style as a string.
    /// </value>
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
    /// <param name="focusEventArgs">
    /// The focus event arguments.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected virtual Task OnFocusAsync(FocusEventArgs? focusEventArgs)
    {
        IsFocused = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the asynchronous event when focus moves out from the element.
    /// </summary>
    /// <param name="focusEventArgs">
    /// The event arguments related to the focus change.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected virtual Task OnFocusOutAsync(FocusEventArgs? focusEventArgs)
    {
        IsFocused = false;
        return Task.CompletedTask;
    }
}