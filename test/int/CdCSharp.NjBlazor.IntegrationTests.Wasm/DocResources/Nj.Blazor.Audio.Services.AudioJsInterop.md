# AudioJsInterop

*Namespace:* Nj.Blazor.Audio.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* AudioJsInterop.cs


---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


**Method:** `SetAudioSourceAsync`
*Method Signature:* `ValueTask SetAudioSourceAsync(ElementReference elRef)`


**Method:** `StartRecordingAsync`
*Method Signature:* `ValueTask StartRecordingAsync()`


**Method:** `StopRecordingAsync`
*Method Signature:* `ValueTask StopRecordingAsync()`


**Method:** `VisualizeCanvasAsync`
*Method Signature:* `ValueTask VisualizeCanvasAsync(ElementReference canvasElementReference)`

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
    


