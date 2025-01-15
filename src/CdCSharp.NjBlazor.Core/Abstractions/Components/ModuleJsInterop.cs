using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Core.Abstractions.Components;

/// <summary>
/// Represents a base class for JavaScript interop modules that implement the IAsyncDisposable interface.
/// </summary>
public abstract class ModuleJsInterop : IAsyncDisposable
{
    /// <summary>
    /// A task completion source used to track the loading status of a module.
    /// </summary>
    protected readonly TaskCompletionSource<bool> IsModuleTaskLoaded = new(false);

    /// <summary>
    /// Represents an interface for invoking JavaScript code from .NET code.
    /// </summary>
    protected readonly IJSRuntime JsRuntime;

    /// <summary>
    /// A lazy asynchronous task that represents a JavaScript object reference.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> ModuleTask;

    /// <summary>
    /// Initializes a new instance of the ModuleJsInterop class.
    /// </summary>
    /// <param name="jsRuntime">
    /// The JavaScript runtime.
    /// </param>
    /// <param name="jsModuleContentPath">
    /// The path to the JavaScript module content.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when jsRuntime is null.
    /// </exception>
    public ModuleJsInterop(IJSRuntime jsRuntime, string jsModuleContentPath)
    {
        JsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        ModuleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", jsModuleContentPath).AsTask());
        IsModuleTaskLoaded.SetResult(true);
    }

    /// <summary>
    /// Asynchronously disposes of the resources used by the module.
    /// </summary>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    public virtual async ValueTask DisposeAsync()
    {
        if (ModuleTask.IsValueCreated)
        {
            IJSObjectReference module = await ModuleTask.Value;
            await module.DisposeAsync();
        }
    }
}