using Microsoft.AspNetCore.Components.Web;

namespace CdCSharp.NjBlazor.Features.Draggable.Abstractions;

/// <summary>Interface for callbacks related to draggable JavaScript elements.</summary>
internal interface INjDraggableJsCallbacks
{
    /// <summary>
    /// Notifies the asynchronous task when a mouse move event occurs.
    /// </summary>
    /// <param name="mouseEventArgs">The MouseEventArgs object containing information about the mouse event.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public Task NotifyMouseMoveAsync(MouseEventArgs mouseEventArgs);
}