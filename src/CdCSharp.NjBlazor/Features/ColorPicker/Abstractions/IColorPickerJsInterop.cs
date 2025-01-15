using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.ColorPicker.Abstractions;

/// <summary>
/// Interface for interacting with a color picker through JavaScript interop.
/// </summary>
public interface IColorPickerJsInterop
{
    /// <summary>
    /// Asynchronously refreshes the position of a handler on a canvas.
    /// </summary>
    /// <param name="canvas">
    /// The reference to the canvas element.
    /// </param>
    /// <param name="element">
    /// The reference to the handler element.
    /// </param>
    /// <param name="clientX">
    /// The client's X-coordinate.
    /// </param>
    /// <param name="clientY">
    /// The client's Y-coordinate.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    ValueTask RefreshHandlerPositionAsync(
        ElementReference canvas,
        ElementReference element,
        double clientX,
        double clientY
    );

    /// <summary>
    /// Removes the relative to window bound position asynchronously.
    /// </summary>
    /// <param name="element">
    /// The element reference.
    /// </param>
    /// <param name="clientX">
    /// The clientX coordinate.
    /// </param>
    /// <param name="clientY">
    /// The clientY coordinate.
    /// </param>
    /// <returns>
    /// An array of doubles representing the removed relative bound position.
    /// </returns>
    ValueTask<double[]> RemoveRelativeBoundPositionAsync(
        ElementReference element,
        double clientX,
        double clientY
    );
}