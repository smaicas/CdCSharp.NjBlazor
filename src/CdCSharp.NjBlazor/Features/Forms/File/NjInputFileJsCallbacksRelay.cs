using CdCSharp.NjBlazor.Features.Forms.File.Abstractions;

using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Features.Forms.File;

/// <summary>
/// Represents a class that relays JavaScript callbacks for input file operations.
/// </summary>
/// <remarks>
/// This class is internal and sealed, meaning it cannot be inherited from and is only accessible
/// within the assembly. Implements the IDisposable interface to allow proper resource cleanup.
/// </remarks>
internal sealed class NjInputFileJsCallbacksRelay : IDisposable
{
    private readonly INjInputFileJsCallbacks _callbacks;

    /// <summary>
    /// Initializes a new instance of the NjInputFileJsCallbacksRelay class.
    /// </summary>
    /// <param name="callbacks">
    /// The callbacks interface for input file operations.
    /// </param>
    [DynamicDependency("NotifyChange")]
    public NjInputFileJsCallbacksRelay(INjInputFileJsCallbacks callbacks)
    {
        _callbacks = callbacks;
        DotNetReference = DotNetObjectReference.Create(this);
    }

    /// <summary>
    /// Gets the .NET reference as an IDisposable object.
    /// </summary>
    /// <value>
    /// The .NET reference as an IDisposable object.
    /// </value>
    public IDisposable DotNetReference { get; }

    /// <summary>
    /// Disposes the underlying DotNetReference object.
    /// </summary>
    public void Dispose() => DotNetReference.Dispose();

    /// <summary>
    /// Notifies a change in the browser files from the JavaScript side.
    /// </summary>
    /// <param name="files">
    /// An array of NjBrowserFile objects representing the changed files.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    [JSInvokable]
    public Task NotifyChange(NjBrowserFile[] files) => _callbacks.NotifyChangeAsync(files);
}