using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Draggable.Abstractions;

/// <summary>
/// Interface for enabling drag and drop functionality in JavaScript interop.
/// </summary>
public interface IDraggableJsInterop
{
    /// <summary>
    /// Disables the mouse move event for the specified element.
    /// </summary>
    /// <param name="element">
    /// The reference to the element for which the mouse move event needs to be disabled.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    ValueTask DisableMouseMoveAsync(ElementReference element);

    /// <summary>
    /// Enables mouse move events for the specified element asynchronously.
    /// </summary>
    /// <param name="element">
    /// The reference to the HTML element on which to enable mouse move events.
    /// </param>
    /// <param name="dotnetObjectReference">
    /// A reference to a .NET object that will handle the mouse move events.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    ValueTask EnableMouseMoveAsync(ElementReference element, object dotnetObjectReference);
}