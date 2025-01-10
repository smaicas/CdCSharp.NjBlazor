# ColorPickerJsInterop

*Namespace:* Nj.Blazor.ColorPicker.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* ColorPickerJsInterop.cs



    Represents a JavaScript interop class for interacting with a color picker module.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for interacting with a color picker module.
    



**Method:** `RefreshHandlerPositionAsync`
*Method Signature:* `ValueTask RefreshHandlerPositionAsync(ElementReference canvas, ElementReference element, Double clientX, Double clientY)`


    Asynchronously refreshes the position of a handler on the canvas.
    



**Method:** `RemoveRelativeBoundPositionAsync`
*Method Signature:* `ValueTask RemoveRelativeBoundPositionAsync(ElementReference element, Double clientX, Double clientY)`


    Asynchronously removes the relative bound position of an element.
    


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
    


