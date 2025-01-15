using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.ColorPicker.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Nj.Blazor.ColorPicker.Services;

/// <summary>
/// Represents a JavaScript interop class for interacting with a color picker module.
/// </summary>
/// <param name="jsRuntime">
/// The JavaScript runtime instance.
/// </param>
/// <seealso cref="ModuleJsInterop" />
/// <seealso cref="IColorPickerJsInterop" />
public class ColorPickerJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.ColorPickerJs),
        IColorPickerJsInterop
{
    /// <summary>
    /// Asynchronously refreshes the position of a handler on the canvas.
    /// </summary>
    /// <param name="canvas">
    /// A reference to the canvas element.
    /// </param>
    /// <param name="element">
    /// A reference to the element whose position needs to be refreshed.
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
    public async ValueTask RefreshHandlerPositionAsync(
        ElementReference canvas,
        ElementReference element,
        double clientX,
        double clientY
    )
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(
            CSharpReferences.Functions.RefreshHandlerPosition,
            canvas,
            element,
            clientX,
            clientY
        );
    }

    /// <summary>
    /// Asynchronously removes the relative bound position of an element.
    /// </summary>
    /// <param name="element">
    /// The reference to the element.
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
    public async ValueTask<double[]> RemoveRelativeBoundPositionAsync(
        ElementReference element,
        double clientX,
        double clientY
    )
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<double[]>(
            CSharpReferences.Functions.RemoveRelativeBoundPosition,
            element,
            clientX,
            clientY
        );
    }
}