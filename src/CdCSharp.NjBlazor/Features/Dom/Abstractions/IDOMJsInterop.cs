using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.Dom.Abstractions;

/// <summary>
/// Interface for JavaScript interop with the Document Object Model (DOM).
/// </summary>
public interface IDOMJsInterop
{
    ValueTask DownloadFileAsync(string fileName, string content, string? contentType);

    ValueTask<(float Top, float Right, float Bottom, float Left)> GetCoordsRelativeAsync(ElementReference relativeTo, string positioning);

    ValueTask<string> GetCssVariableAsync(string variableName);

    ValueTask FocusElementAsync(string querySelector, ElementReference? parentElement = null);

    ValueTask<string> GetFocusedElementClassAsync();

    ValueTask InputFileInitializeCallbacksAsync(object dotnetObjectReference, ElementReference element);

    ValueTask<IJSStreamReference> ReadFileDataAsync(CancellationToken ct, ElementReference element, int fileId);

    ValueTask<string> ReadImageDataAsync(ElementReference element, int fileId);

    ValueTask ScrollToClosestAsync(string querySelector, ElementReference? element);

    ValueTask ScrollTopAsync(ElementReference? element, int? position = 0);

    ValueTask SetCssVariableAsync(string variableName, string value);

    ValueTask SetDisabledAsync(ElementReference element, bool value);

    ValueTask SetCalendarDatepickerValue(ElementReference calendarInputRef, string value);

    ValueTask AddShowPickerEventHandler(ElementReference clickElementRef, ElementReference inputCalendarElementRef);

    ValueTask SelectText(ElementReference element);

    ValueTask<(float Width, float Height)> GetElementBounds(string queryElement);
}