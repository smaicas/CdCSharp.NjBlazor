# DomJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.Dom.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* DomJsInterop.cs


---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


**Method:** `AddShowPickerEventHandler`
*Method Signature:* `ValueTask AddShowPickerEventHandler(ElementReference clickElementRef, ElementReference inputCalendarElementRef)`


**Method:** `DownloadFileAsync`
*Method Signature:* `ValueTask DownloadFileAsync(String fileName, String content, String contentType)`


**Method:** `FocusElementAsync`
*Method Signature:* `ValueTask FocusElementAsync(String querySelector, Nullable parentElement)`


    Asynchronously focuses on an element specified by the query selector.
    



**Method:** `GetCoordsRelativeAsync`
*Method Signature:* `ValueTask GetCoordsRelativeAsync(ElementReference relativeTo, String positioning)`


**Method:** `GetCssVariableAsync`
*Method Signature:* `ValueTask GetCssVariableAsync(String variableName)`


**Method:** `GetElementBounds`
*Method Signature:* `ValueTask GetElementBounds(String queryElement)`


**Method:** `GetFocusedElementClassAsync`
*Method Signature:* `ValueTask GetFocusedElementClassAsync()`


**Method:** `InputFileInitializeCallbacksAsync`
*Method Signature:* `ValueTask InputFileInitializeCallbacksAsync(Object dotnetObjectReference, ElementReference element)`


**Method:** `ReadFileDataAsync`
*Method Signature:* `ValueTask ReadFileDataAsync(CancellationToken ct, ElementReference element, Int32 fileId)`


**Method:** `ReadImageDataAsync`
*Method Signature:* `ValueTask ReadImageDataAsync(ElementReference element, Int32 fileId)`


**Method:** `ScrollToClosestAsync`
*Method Signature:* `ValueTask ScrollToClosestAsync(String querySelector, Nullable element)`


**Method:** `ScrollTopAsync`
*Method Signature:* `ValueTask ScrollTopAsync(Nullable element, Nullable position)`


**Method:** `SelectText`
*Method Signature:* `ValueTask SelectText(ElementReference element)`


**Method:** `SetCalendarDatepickerValue`
*Method Signature:* `ValueTask SetCalendarDatepickerValue(ElementReference calendarInputRef, String value)`


**Method:** `SetCssVariableAsync`
*Method Signature:* `ValueTask SetCssVariableAsync(String variableName, String value)`


**Method:** `SetDisabledAsync`
*Method Signature:* `ValueTask SetDisabledAsync(ElementReference element, Boolean value)`

---
## Inherited from ModuleJsInterop

**Summary:**

    Represents a base class for JavaScript interop modules that implement the IAsyncDisposable interface.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime, String jsModuleContentPath)`


    Initializes a new instance of the ModuleJsInterop class.
    



**Method:** `DisposeAsync`
*Method Signature:* `ValueTask DisposeAsync()`


    Asynchronously disposes of the resources used by the module.
    


