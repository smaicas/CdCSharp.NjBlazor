using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.TextPattern.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.TextPattern.Services;

/// <summary>
/// Represents a JavaScript interop class for text pattern operations.
/// </summary>
/// <param name="jsRuntime">
/// The JavaScript runtime instance.
/// </param>
/// <seealso cref="ModuleJsInterop" />
/// <seealso cref="ITextPatternJsInterop" />
public class TextPatternJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.TextPatternJs), ITextPatternJsInterop
{
    /// <summary>
    /// Adds dynamic text patterns to the specified content box using the provided elements.
    /// </summary>
    /// <param name="contentBox">
    /// The reference to the content box element.
    /// </param>
    /// <param name="elements">
    /// The collection of element patterns to add dynamically.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    public async ValueTask TextPatternAddDynamic(
        ElementReference contentBox,
        IEnumerable<ElementPattern> elements,
        object? dotnetReference,
        string notifyChangedTextCallback,
        string validatePartialCallback)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(
            CSharpReferences.Functions.TextPatternAddDynamic,
            contentBox,
            elements,
            dotnetReference,
            notifyChangedTextCallback,
            validatePartialCallback);
    }
}