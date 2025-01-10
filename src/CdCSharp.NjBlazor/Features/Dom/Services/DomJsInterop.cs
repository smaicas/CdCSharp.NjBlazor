using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.Dom.Services;

public class DomJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.DomJs), IDOMJsInterop
{
    public async ValueTask DownloadFileAsync(string fileName, string content, string? contentType)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.DownloadFile, fileName, content, contentType);
    }

    public async ValueTask<(float Top, float Right, float Bottom, float Left)> GetCoordsRelativeAsync(ElementReference relativeTo, string positioning)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        float[] coords = await JsRuntime.InvokeAsync<float[]>(CSharpReferences.Functions.GetCoordsRelative, relativeTo, positioning);
        return (coords[0], coords[1], coords[2], coords[3]);
    }

    public async ValueTask<string> GetCssVariableAsync(string variableName)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        string cssVar = await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.GetCssVariable, variableName);
        return cssVar;
    }

    /// <summary>
    /// Asynchronously focuses on an element specified by the query selector.
    /// </summary>
    /// <param name="querySelector">The query selector to identify the element to focus on.</param>
    /// <param name="parentElement">Optional. The parent element reference where the query selector will be applied.</param>
    /// <returns>An asynchronous operation representing the focus action on the specified element.</returns>
    public async ValueTask FocusElementAsync(
        string querySelector,
        ElementReference? parentElement = null
    )
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(
            CSharpReferences.Functions.FocusElement,
            querySelector,
            parentElement
        );
    }

    public async ValueTask<string> GetFocusedElementClassAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        string cssClassList = await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.GetFocusedElementClass);
        return cssClassList;
    }

    public async ValueTask InputFileInitializeCallbacksAsync(object dotnetObjectReference, ElementReference element)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.InputFileInitializeCallbacks, dotnetObjectReference, element);
    }

    public async ValueTask<IJSStreamReference> ReadFileDataAsync(CancellationToken ct, ElementReference element, int fileId)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        IJSStreamReference jsStream = await JsRuntime.InvokeAsync<IJSStreamReference>(CSharpReferences.Functions.ReadFileData, ct, element, fileId);
        return jsStream;
    }

    public async ValueTask<string> ReadImageDataAsync(ElementReference element, int fileId)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        string imageData = await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.ReadImageData, element, fileId);
        return imageData;
    }

    public async ValueTask ScrollToClosestAsync(string querySelector, ElementReference? element)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.ScrollToClosest, querySelector, element);
    }

    public async ValueTask ScrollTopAsync(ElementReference? element, int? position = 0)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.ScrollTop, element, position);
    }

    public async ValueTask SelectText(ElementReference element)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.SelectText, element);
    }

    public async ValueTask SetCssVariableAsync(string variableName, string value)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.SetCssVariable, variableName, value);
    }

    public async ValueTask SetDisabledAsync(ElementReference element, bool value)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.SetDisabled, element, value);
    }

    public async ValueTask AddShowPickerEventHandler(ElementReference clickElementRef, ElementReference inputCalendarElementRef)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.AddShowPickerEventHandler, clickElementRef, inputCalendarElementRef);
    }
    public async ValueTask SetCalendarDatepickerValue(ElementReference calendarInputRef, string value)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.AddShowPickerEventHandler, calendarInputRef, value);
    }

    public async ValueTask<(float Width, float Height)> GetElementBounds(string queryElement)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        float[] coords = await JsRuntime.InvokeAsync<float[]>(CSharpReferences.Functions.GetElementBounds, queryElement);
        return (coords[0], coords[1]);
    }
}