using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Text;

/// <summary>
/// Base class for handling input of text values.
/// </summary>
/// <typeparam name="string">
/// The type of input handled by this class.
/// </typeparam>
public abstract class NjInputTextBase : NjInputBase<string>
{
    /// <summary>
    /// Gets or sets a value indicating whether the input is a password.
    /// </summary>
    /// <value>
    /// True if the input is a password; otherwise, false.
    /// </value>
    [Parameter]
    public bool IsPassword { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the input is a text area.
    /// </summary>
    /// <value>
    /// True if the input is a text area; otherwise, false.
    /// </value>
    [Parameter]
    public bool IsTextArea { get; set; }

    /// <summary>
    /// Gets or sets the label associated with the parameter.
    /// </summary>
    /// <value>
    /// The label associated with the parameter.
    /// </value>
    [Parameter]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the placeholder text for the input field.
    /// </summary>
    /// <value>
    /// The placeholder text.
    /// </value>
    [Parameter]
    public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the post adornment for the element.
    /// </summary>
    /// <value>
    /// The post adornment string.
    /// </value>
    [Parameter]
    public string? PostAdornment { get; set; }

    /// <summary>
    /// Gets or sets the color of the post adornment.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color of the post adornment.
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
    /// Gets or sets the color of the pre-adornment for the component.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color of the pre-adornment.
    /// </value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title.
    /// </value>
    [Parameter]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the DOM JavaScript interop service.
    /// </summary>
    /// <remarks>
    /// This property is injected with an instance of the IDOMJsInterop service.
    /// </remarks>
    [Inject]
    protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>
    /// Determines if the current value is empty.
    /// </summary>
    /// <returns>
    /// True if the current value is empty; otherwise, false.
    /// </returns>
    protected override bool IsEmpty => string.IsNullOrEmpty(CurrentValue);

    /// <summary>
    /// Determines the CSS class for the password field based on the IsPassword property.
    /// </summary>
    /// <value>
    /// The CSS class for the password field.
    /// </value>
    protected string PasswordClass => IsPassword ? CssClassReferences.Text.Password : string.Empty;

    /// <summary>
    /// Gets the CSS class for the text area element based on the IsTextArea property.
    /// </summary>
    /// <value>
    /// If IsTextArea is true, returns the CSS class reference for text area styling; otherwise,
    /// returns an empty string.
    /// </value>
    protected string TextAreaClass => IsTextArea ? CssClassReferences.Text.TextArea : string.Empty;

    /// <summary>
    /// Asynchronously handles input changes and updates the current value.
    /// </summary>
    /// <param name="changeEventArgs">
    /// The event arguments containing the changed value.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected override async Task OnInputAsync(ChangeEventArgs changeEventArgs) =>
        await UpdateCurrentValue(changeEventArgs.Value?.ToString());

    /// <summary>
    /// Tries to parse a string value.
    /// </summary>
    /// <param name="value">
    /// The string value to parse.
    /// </param>
    /// <param name="result">
    /// The parsed result.
    /// </param>
    /// <param name="validationErrorMessage">
    /// The validation error message.
    /// </param>
    /// <returns>
    /// True if the parsing was successful; otherwise, false.
    /// </returns>
    protected override bool TryParseValueFromString(
        string? value,
        out string result,
        out string validationErrorMessage
    )
    {
        result = value ?? string.Empty;
        validationErrorMessage = string.Empty;
        return true;
    }

    /// <summary>
    /// Updates the current value with the new value if they are different.
    /// </summary>
    /// <param name="newValue">
    /// The new value to update to.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task UpdateCurrentValue(string? newValue)
    {
        if (CurrentValue != newValue)
            CurrentValue = newValue;

        return Task.CompletedTask;
    }
}