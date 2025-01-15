using CdCSharp.NjBlazor.Core.Abstractions.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Color;

/// <summary>
/// Base class for handling input of color values.
/// </summary>
/// <typeparam name="System.Drawing.Color">
/// The type of color being handled.
/// </typeparam>
public abstract class NjInputColorBase : NjInputBase<System.Drawing.Color>
{
    /// <summary>
    /// Indicates whether a specific operation is open or not.
    /// </summary>
    protected bool _open;

    private System.Drawing.Color _prevColor = System.Drawing.Color.Red;

    /// <summary>
    /// Accepts the picker selection.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    protected Task AcceptPicker()
    {
        _open = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Cancels the color picker operation and reverts to the previous color value.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous cancel operation.
    /// </returns>
    protected Task CancelPicker()
    {
        CurrentValue = _prevColor;
        _open = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// This method is called when parameters are set. If the current value is the default value, it
    /// is set to the color red.
    /// </summary>
    protected override void OnParametersSet()
    {
        if (CurrentValue == default)
        {
            CurrentValue = System.Drawing.Color.Red;
        }
    }

    /// <summary>
    /// Toggles the color picker.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    protected Task TogglePicker()
    {
        if (ReadOnly)
            return Task.CompletedTask;
        _open = !_open;

        if (_open)
            _prevColor = CurrentValue;
        return Task.CompletedTask;
    }
}