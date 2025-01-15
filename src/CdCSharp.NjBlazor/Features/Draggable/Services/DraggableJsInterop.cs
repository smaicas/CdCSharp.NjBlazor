using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Draggable.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.Draggable.Services;

/// <summary>
/// Represents a JavaScript interop class for handling draggable elements.
/// </summary>
/// <param name="jsRuntime">
/// The JavaScript runtime instance.
/// </param>
/// <seealso cref="ModuleJsInterop" />
/// <seealso cref="IDraggableJsInterop" />
public class DraggableJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.DraggableJs),
        IDraggableJsInterop
{
    /// <summary>
    /// Disables mouse move events for a specified HTML element asynchronously.
    /// </summary>
    /// <param name="element">
    /// The reference to the HTML element for which mouse move events should be disabled.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    public async ValueTask DisableMouseMoveAsync(ElementReference element)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.DisableMouseMove, element);
    }

    /// <summary>
    /// Enables mouse move functionality for a specified element asynchronously.
    /// </summary>
    /// <param name="element">
    /// The reference to the element for which mouse move functionality is enabled.
    /// </param>
    /// <param name="dotnetObjectReference">
    /// The reference to the .NET object associated with the element.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    public async ValueTask EnableMouseMoveAsync(
        ElementReference element,
        object dotnetObjectReference
    )
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(
            CSharpReferences.Functions.EnableMouseMove,
            element,
            dotnetObjectReference
        );
    }
}