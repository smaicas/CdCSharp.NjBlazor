using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Range;

/// <summary>
/// Base class for input range components that handle double values.
/// </summary>
public abstract class NjInputRangeBase : NjInputBase<double>
{
    /// <summary>
    /// A protected field to store the reference ID of the input.
    /// </summary>
    protected string? _inputReferenceId;

    /// <summary>Gets or sets the label associated with the parameter.</summary>
    /// <value>The label associated with the parameter.</value>
    [Parameter]
    public string Label { get; set; } = string.Empty;

    /// <summary>Gets or sets the maximum value.</summary>
    /// <value>The maximum value allowed.</value>
    [Parameter]
    public double Max { get; set; } = 100;

    /// <summary>Gets or sets the minimum value.</summary>
    /// <value>The minimum value.</value>
    [Parameter]
    public double Min { get; set; } = 0;

    /// <summary>Gets or sets the step value.</summary>
    /// <value>The step value.</value>
    [Parameter]
    public double Step { get; set; } = 1;

    /// <summary>
    /// Gets or sets the DOM JavaScript interop service.
    /// </summary>
    /// <remarks>
    /// This property is injected with an instance of the IDOMJsInterop interface.
    /// </remarks>
    [Inject]
    protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>Invoked after the component has been rendered.</summary>
    /// <param name="firstRender">A boolean value indicating whether this is the first render of the component.</param>
    /// <remarks>
    /// This method is called after the component has been rendered. If it is the first render and the InputReference is not null,
    /// the _inputReferenceId is set to the Id of the InputReference and the component's state is refreshed.
    /// </remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender && InputReference != null)
        {
            _inputReferenceId = InputReference.Value.Id;
            StateHasChanged();
        }
    }

    /// <summary>
    /// Method called after rendering the component asynchronously.
    /// </summary>
    /// <param name="firstRender">A boolean value indicating if this is the first render.</param>
    /// <returns>An asynchronous task.</returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (ReadOnly && InputReference != null)
        {
            await DomJs.SetDisabledAsync(InputReference.Value, true);
        }
    }

    /// <summary>
    /// Tries to parse a value from a string to a double.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="result">When this method returns, contains the double value equivalent of the string, if the conversion succeeded, or zero if the conversion failed.</param>
    /// <param name="validationErrorMessage">When this method returns, contains any validation error message.</param>
    /// <returns>True if the parsing was successful; otherwise, false.</returns>
    protected override bool TryParseValueFromString(
        string? value,
        out double result,
        out string validationErrorMessage
    )
    {
        validationErrorMessage = string.Empty;
        result = 0;
        if (!string.IsNullOrEmpty(value))
            result = double.Parse(value);
        return true;
    }
}
