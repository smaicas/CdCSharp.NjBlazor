using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Number;

/// <summary>
/// Base class for input components that handle numeric values.
/// </summary>
/// <typeparam name="double?">
/// The type of numeric value handled by the input component.
/// </typeparam>
public abstract class NjInputNumberBase : NjInputBase<double?>
{
    private bool _processDecrease;
    private bool _processIncrease;

    /// <summary>
    /// Gets or sets the DOM JavaScript interop service.
    /// </summary>
    /// <value>
    /// The DOM JavaScript interop service.
    /// </value>
    [Inject]
    public IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>
    /// Gets or sets the label to display
    /// </summary>
    /// <value>
    /// The label to display.
    /// </value>
    [Parameter]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum value allowed.
    /// </summary>
    /// <value>
    /// The maximum value allowed.
    /// </value>
    [Parameter]
    public double Max { get; set; } = double.MaxValue;

    /// <summary>
    /// Gets or sets the minimum value.
    /// </summary>
    /// <value>
    /// The minimum value.
    /// </value>
    [Parameter]
    public double Min { get; set; } = double.MinValue;

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
    /// <remarks>
    /// The default value is set to the Material Icons arrow circle right icon.
    /// </remarks>
    [Parameter]
    public string? PostAdornment { get; set; } =
        Media.Icons.NjIcons.Materials.MaterialIcons.i_arrow_circle_right;

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
    public string? PreAdornment { get; set; } =
        Media.Icons.NjIcons.Materials.MaterialIcons.i_arrow_circle_left;

    /// <summary>
    /// Gets or sets the color of the pre-adornment for the component.
    /// </summary>
    /// <value>
    /// A nullable CssColor representing the color of the pre-adornment.
    /// </value>
    [Parameter]
    public CssColor? PreAdornmentColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show arrows.
    /// </summary>
    /// <value>
    /// True if arrows should be shown; otherwise, false.
    /// </value>
    [Parameter]
    public bool ShowArrows { get; set; } = true;

    /// <summary>
    /// Gets or sets the step value.
    /// </summary>
    /// <value>
    /// The step value.
    /// </value>
    [Parameter]
    public double Step { get; set; } = 1.0d;

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>
    /// The title of the object.
    /// </value>
    [Parameter]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Asynchronously decreases the current value by the specified step until the process is stopped.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <remarks>
    /// If the property ReadOnly is set to true, the method returns immediately without performing
    /// any operation. The method continuously decreases the current value by the specified step
    /// until the process is stopped. The delay between each decrease operation is gradually reduced
    /// by 50 milliseconds.
    /// </remarks>
    protected async Task DecreaseByStepAsync()
    {
        if (ReadOnly)
            return;

        _processDecrease = false;
        _processDecrease = true;
        int wait = 50;

        while (_processDecrease)
        {
            await UpdateCurrentValue(Math.Max(Min, (CurrentValue ?? 0) - Step));
            await Task.Delay(50 + wait);
            wait--;
        }
    }

    /// <summary>
    /// Increases the current value by the step asynchronously.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <remarks>
    /// If the property ReadOnly is set to true, the method returns immediately without performing
    /// any operation. The method continuously increases the current value by the specified step
    /// until the process is stopped. The delay between each decrease operation is gradually reduced
    /// by 50 milliseconds.
    /// </remarks>
    protected async Task IncreaseByStepAsync()
    {
        if (ReadOnly)
            return;

        _processIncrease = false;
        _processIncrease = true;
        int wait = 50;

        while (_processIncrease)
        {
            await UpdateCurrentValue(Math.Min(Max, (CurrentValue ?? 0) + Step));
            await Task.Delay(50 + wait);
            wait--;
        }
    }

    /// <summary>
    /// Stops the asynchronous process of decreasing.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    protected Task StopDecreaseAsync()
    {
        _processDecrease = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops the process of increasing asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    protected Task StopIncreaseAsync()
    {
        _processIncrease = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Tries to parse a value from a string to a nullable double.
    /// </summary>
    /// <param name="value">
    /// The string value to parse.
    /// </param>
    /// <param name="result">
    /// The parsed double value, if successful; otherwise, null.
    /// </param>
    /// <param name="validationErrorMessage">
    /// An error message if parsing fails; otherwise, an empty string.
    /// </param>
    /// <returns>
    /// True if the parsing was successful; otherwise, false.
    /// </returns>
    protected override bool TryParseValueFromString(
        string? value,
        out double? result,
        out string validationErrorMessage
    )
    {
        validationErrorMessage = string.Empty;
        result = null;
        if (!string.IsNullOrEmpty(value))
            result = double.Parse(value);
        return true;
    }

    /// <summary>
    /// Updates the current value with a new value if it is different.
    /// </summary>
    /// <param name="newValue">
    /// The new value to update the current value with.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task UpdateCurrentValue(double? newValue)
    {
        if (CurrentValue != newValue)
            CurrentValue = newValue;

        return Task.CompletedTask;
    }
}