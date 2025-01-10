using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace CdCSharp.NjBlazor.Features.Forms.Dropdown;

/// <summary>
/// Base class for a dropdown input component that allows selecting a value of type TValue.
/// </summary>
/// <typeparam name="TValue">The type of value that can be selected.</typeparam>
public abstract class NjInputDropdownBase<TValue> : NjInputBase<TValue>
{
    /// <summary>Indicates whether multiple selection is allowed.</summary>
    public readonly bool IsMultipleSelection;

    /// <summary>
    /// List of options for an input dropdown.
    /// </summary>
    public readonly List<NjInputDropdownOption> Options = [];

    /// <summary>Reference to an HTML element.</summary>
    protected ElementReference BoxReference;

    /// <summary>Coordinates of the options box.</summary>
    /// <value>A tuple representing the top, right, bottom, and left coordinates of the options box.</value>
    protected (float Top, float Right, float Bottom, float Left) OptionsBoxCoords = (
        0f,
        0f,
        0f,
        0f
    );

    private readonly List<object> CurrentValueMultiple = [];

    private bool _processingSelection;

    /// <summary>
    /// Initializes a new instance of the NjInputDropdownBase class.
    /// Sets the IsMultipleSelection property based on whether the type TValue is an array.
    /// </summary>
    public NjInputDropdownBase() => IsMultipleSelection = typeof(TValue).IsArray;

    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>Gets or sets a value indicating whether the input is clearable.</summary>
    /// <value>True if the input is clearable; otherwise, false.</value>
    [Parameter]
    public bool Clearable { get; set; } = true;

    /// <summary>
    /// Gets or sets a function that defines how an item should be displayed.
    /// </summary>
    /// <value>
    /// A function that takes an object as input and returns a string representing how the item should be displayed.
    /// </value>
    [Parameter]
    public Func<object, string>? ItemDisplay { get; set; }

    /// <summary>Gets or sets the label associated with the parameter.</summary>
    /// <value>The label associated with the parameter.</value>
    [Parameter]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the position of the dropdown options.
    /// </summary>
    /// <value>
    /// The position of the dropdown options.
    /// </value>
    [Parameter]
    public NjDropdownOptionsPosition OptionsPosition { get; set; } =
        NjDropdownOptionsPosition.Center;

    /// <summary>
    /// Gets or sets the post-adornment string.
    /// </summary>
    /// <value>The post-adornment string.</value>
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

    /// <summary>Gets or sets the string to be displayed before the main content.</summary>
    /// <value>The string to be displayed before the main content.</value>
    [Parameter]
    public string? PreAdornment { get; set; }

    /// <summary>Gets or sets the color of the pre-adornment.</summary>
    /// <value>The color of the pre-adornment.</value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>
    /// Generates a dictionary of active classes based on the options and current values.
    /// </summary>
    /// <returns>A dictionary where the key is an object and the value is a string representing the active class.</returns>
    protected Dictionary<object, string> ActiveClass =>
        Options
            .Where(op => op.Value != null)
            .ToDictionary(
                o => o.Value!,
                o =>
                    CurrentValueMultiple.Contains(o.Value!)
                        ? CssClassReferences.Active
                        : string.Empty
            );

    /// <summary>
    /// Gets or sets the DOM JavaScript interop service.
    /// </summary>
    /// <remarks>
    /// This property is injected with an instance of the IDOMJsInterop interface.
    /// </remarks>
    [Inject]
    protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>Gets or sets a value indicating whether the object is open.</summary>
    /// <value>True if the object is open; otherwise, false.</value>
    protected bool IsOpen { get; set; }

    /// <summary>Gets the CSS style for the options box based on its coordinates.</summary>
    /// <value>The CSS style for the options box.</value>
    protected string OptionsBoxStyle
    {
        get
        {
            StringBuilder sb = new();

            if (OptionsBoxCoords.Top > 0f)
            {
                sb.Append($"top: {OptionsBoxCoords.Top.ToString(CultureInfo.InvariantCulture)}px;");
            }
            if (OptionsBoxCoords.Right > 0f)
            {
                sb.Append(
                    $"right: {OptionsBoxCoords.Right.ToString(CultureInfo.InvariantCulture)}px;"
                );
            }
            if (OptionsBoxCoords.Bottom > 0f)
            {
                sb.Append(
                    $"bottom: {OptionsBoxCoords.Bottom.ToString(CultureInfo.InvariantCulture)}px;"
                );
            }
            if (OptionsBoxCoords.Left > 0f)
            {
                sb.Append(
                    $"left: {OptionsBoxCoords.Left.ToString(CultureInfo.InvariantCulture)}px;"
                );
            }

            return sb.ToString();
        }
    }

    /// <summary>Adds an option to the dropdown list if it doesn't already exist.</summary>
    /// <param name="option">The option to add to the dropdown list.</param>
    public void AddOption(NjInputDropdownOption option)
    {
        if (Options.Contains(option))
            return;
        Options.Add(option);
    }

    /// <summary>
    /// Formats the specified value as a string.
    /// </summary>
    /// <param name="value">The value to be formatted.</param>
    /// <returns>The formatted value as a string.</returns>
    protected override string? FormatValueAsString(TValue? value)
    {
        if (IsMultipleSelection)
        {
            string res = string.Join(",", value);
            return res;
        }
        return base.FormatValueAsString(value);
    }

    /// <summary>
    /// Asynchronously retrieves the coordinates of the options box relative to a reference element.
    /// </summary>
    /// <returns>
    /// A tuple containing the coordinates (left, top, right, bottom) of the options box.
    /// </returns>
    protected async Task<(float, float, float, float)> GetOptionsBoxCoordsAsync() =>
        await DomJs.GetCoordsRelativeAsync(BoxReference, OptionsPosition.ToString().ToLower());

    /// <summary>
    /// Method called after rendering the component asynchronously.
    /// </summary>
    /// <param name="firstRender">A boolean value indicating if this is the first render.</param>
    /// <returns>An asynchronous Task.</returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (!firstRender && !IsOpen && IsMultipleSelection && _processingSelection)
        {
            IsOpen = true;
            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// Asynchronously handles the focus event.
    /// </summary>
    /// <param name="focusEventArgs">The focus event arguments.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task OnFocusAsync(FocusEventArgs? focusEventArgs)
    {
        if (ReadOnly)
            return;
        await base.OnFocusAsync(focusEventArgs);
    }

    /// <summary>
    /// Asynchronously handles the event when the component loses focus.
    /// </summary>
    /// <param name="focusEventArgs">The focus event arguments.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task OnFocusOutAsync(FocusEventArgs? focusEventArgs)
    {
        await base.OnFocusOutAsync(focusEventArgs);
        _processingSelection = false;
        IsOpen = false;
    }

    /// <summary>
    /// Selects an option asynchronously.
    /// </summary>
    /// <param name="value">The value of the option to select.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected async Task SelectOptionAsync(object? value)
    {
        _processingSelection = true;

        if (InputReference != null)
        {
            await InputReference.Value.FocusAsync();
        }

        if (value == default)
        {
            CurrentValue = default;
            CurrentValueMultiple.Clear();
            return;
        }

        if (IsMultipleSelection)
        {
            if (CurrentValueMultiple.Contains(value))
                CurrentValueMultiple.Remove(value);
            else
                CurrentValueMultiple.Add(value);
            SetCurrentValueAsStringArray(CurrentValueMultiple.ToArray());
        }
        else
        {
            CurrentValue = (TValue)value;
        }
    }

    /// <summary>
    /// Asynchronously displays the item's value.
    /// </summary>
    /// <param name="value">The value to display.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the displayed item as a string.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the item's value is null and no custom display function is provided.</exception>
    protected Task<string> ShowItemDisplayAsync(object value)
    {
        if (ItemDisplay != null)
            return Task.FromResult(ItemDisplay(value));
        else
            return Task.FromResult(
                value?.ToString()
                    ?? throw new InvalidOperationException(
                        $"{nameof(ShowItemDisplayAsync)} can't ToString(). Resulted null"
                    )
            );
    }

    /// <summary>
    /// Toggles the dropdown menu asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected async Task ToggleDropdownAsync()
    {
        if (ReadOnly)
            return;
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            OptionsBoxCoords = await GetOptionsBoxCoordsAsync();
        }
    }

    /// <summary>
    /// Tries to parse a value from a string representation.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">When this method returns, contains the parsed value if the parsing succeeded, otherwise the default value.</param>
    /// <param name="validationErrorMessage">When this method returns, contains an error message if the parsing failed, otherwise an empty string.</param>
    /// <returns>True if the parsing was successful; otherwise, false.</returns>
    protected override bool TryParseValueFromString(
        string? value,
        out TValue result,
        out string validationErrorMessage
    ) => this.TryParseSelectableValueFromString(value, out result, out validationErrorMessage);

    private void SetCurrentValueAsStringArray(object?[]? value)
    {
        CurrentValue = BindConverter.TryConvertTo<TValue>(
            value,
            CultureInfo.CurrentCulture,
            out TValue? result
        )
            ? result
            : default;
    }
}

internal static class InputExtensions
{
    public static bool TryParseSelectableValueFromString<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TValue
    >(
        this NjInputBase<TValue> input,
        string? value,
        [MaybeNullWhen(false)] out TValue result,
        [NotNullWhen(false)] out string? validationErrorMessage
    )
    {
        try
        {
            if (typeof(TValue) == typeof(bool))
                if (TryConvertToBool(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else if (typeof(TValue) == typeof(bool?))
                    if (TryConvertToNullableBool(value, out result))
                    {
                        validationErrorMessage = null;
                        return true;
                    }
                    else if (typeof(TValue).IsArray)
                        if (TryConvertToArray(value, out result))
                        {
                            validationErrorMessage = null;
                            return true;
                        }
                        else if (
                            BindConverter.TryConvertTo<TValue>(
                                value,
                                CultureInfo.CurrentCulture,
                                out TValue? parsedValue
                            )
                        )
                        {
                            result = parsedValue;
                            validationErrorMessage = null;
                            return true;
                        }

            result = default;
            validationErrorMessage =
                $"The {input.DisplayName ?? input.FieldIdentifier.FieldName} field is not valid.";
            return false;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(
                $"{input.GetType()} does not support the type '{typeof(TValue)}'.",
                ex
            );
        }
    }

    private static bool TryConvertToArray<TValue>(string? value, out TValue result)
    {
        if (value == null)
        {
            result = default!;
            return false;
        }

        string trimmedValue = value.TrimStart('[').TrimEnd(']');

        string[] parts = trimmedValue.Split(',');

        Type elementType = typeof(TValue).GetElementType();

        if (elementType == null)
            throw new InvalidOperationException($"Type {typeof(TValue)} is not an array.");

        Array array = Array.CreateInstance(elementType, parts.Length);

        for (int i = 0; i < parts.Length; i++)
        {
            object convertedValue = Convert.ChangeType(parts[i], elementType);
            array.SetValue(convertedValue, i);
        }

        result = (TValue)(object)array;

        return true;
    }

    private static bool TryConvertToBool<TValue>(string? value, out TValue result)
    {
        if (bool.TryParse(value, out bool @bool))
        {
            result = (TValue)(object)@bool;
            return true;
        }

        result = default!;
        return false;
    }

    private static bool TryConvertToNullableBool<TValue>(string? value, out TValue result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = default!;
            return true;
        }

        return TryConvertToBool(value, out result);
    }
}
