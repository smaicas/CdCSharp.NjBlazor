using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.DeviceManager.Components;

/// <summary>
/// Represents a class that relays resize callbacks.
/// </summary>
/// <remarks>
/// This class implements the IDisposable interface.
/// </remarks>
public class ResizeCallbacksRelay : IDisposable
{
    private readonly IResizeJsCallback _callbacks;

    /// <summary>Initializes a new instance of the ResizeCallbacksRelay class.</summary>
    /// <param name="callbacks">The callbacks interface for resize events.</param>
    [DynamicDependency("NotifyResize")]
    public ResizeCallbacksRelay(IResizeJsCallback callbacks)
    {
        _callbacks = callbacks;
        DotNetReference = DotNetObjectReference.Create(this);
    }

    /// <summary>Gets the .NET reference as an IDisposable.</summary>
    /// <returns>The .NET reference as an IDisposable.</returns>
    public IDisposable DotNetReference { get; }

    /// <summary>
    /// Disposes the underlying DotNetReference object.
    /// </summary>
    public void Dispose() => DotNetReference.Dispose();

    /// <summary>
    /// Notifies the callback function about a window resize event.
    /// </summary>
    /// <param name="windowWidth">The width of the window.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [JSInvokable]
    public Task NotifyResize(int windowWidth) => _callbacks.NotifyResize(windowWidth);
}
