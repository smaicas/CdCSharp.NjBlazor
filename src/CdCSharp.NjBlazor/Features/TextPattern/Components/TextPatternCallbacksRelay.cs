using CdCSharp.NjBlazor.Features.TextPattern.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Features.DeviceManager.Components;

/// <summary>
/// Represents a class that relays resize callbacks.
/// </summary>
/// <remarks>
/// This class implements the IDisposable interface.
/// </remarks>
public class TextPatternCallbacksRelay : IDisposable
{
    private readonly ITextPatternJsCallback _callbacks;

    /// <summary>Initializes a new instance of the ResizeCallbacksRelay class.</summary>
    /// <param name="callbacks">The callbacks interface for resize events.</param>
    [DynamicDependency("NotifyTextChanged")]
    [DynamicDependency("ValidatePartial")]
    public TextPatternCallbacksRelay(ITextPatternJsCallback callbacks)
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
    public Task NotifyTextChanged(string text) => _callbacks.NotifyTextChanged(text);

    /// <summary>
    /// Validates partial pattern.
    /// </summary>
    /// <param name="windowWidth">The width of the window.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    [JSInvokable]
    public Task ValidatePartial(int index, string text) => _callbacks.ValidatePartial(index, text);
}
