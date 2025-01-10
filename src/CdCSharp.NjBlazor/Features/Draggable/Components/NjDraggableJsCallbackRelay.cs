using CdCSharp.NjBlazor.Features.Draggable.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Features.Draggable.Components;

/// <summary>
/// Represents a class that acts as a relay for draggable JavaScript callbacks.
/// </summary>
/// <remarks>
/// This class is used internally to manage draggable JavaScript callbacks.
/// </remarks>
/// <seealso cref="System.IDisposable"/>
internal class NjDraggableJsCallbackRelay : IDisposable
{
    private readonly INjDraggableJsCallbacks _callbacks;

    /// <summary>Initializes a new instance of the NjDraggableJsCallbackRelay class.</summary>
    /// <param name="callbacks">The callbacks interface.</param>
    [DynamicDependency("NotifyMouseMove")]
    public NjDraggableJsCallbackRelay(INjDraggableJsCallbacks callbacks)
    {
        _callbacks = callbacks;
        DotNetReference = DotNetObjectReference.Create(this);
    }

    /// <summary>Gets the .NET reference as an IDisposable object.</summary>
    /// <value>The .NET reference as an IDisposable object.</value>
    public IDisposable DotNetReference { get; }

    /// <summary>
    /// Disposes the underlying DotNetReference object.
    /// </summary>
    public void Dispose() => DotNetReference.Dispose();

    /// <summary>
    /// Notifies the client of a mouse move event.
    /// </summary>
    /// <param name="clientX">The horizontal coordinate of the mouse pointer.</param>
    /// <param name="clientY">The vertical coordinate of the mouse pointer.</param>
    [JSInvokable]
    public void NotifyMouseMove(int clientX, int clientY)
    {
        MouseEventArgs args = new() { ClientX = clientX, ClientY = clientY };
        _callbacks.NotifyMouseMoveAsync(args);
    }
}