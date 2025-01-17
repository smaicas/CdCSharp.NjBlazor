# DraggableJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.Draggable.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* DraggableJsInterop.cs



    Represents a JavaScript interop class for handling draggable elements.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for handling draggable elements.
    



**Method:** `DisableMouseMoveAsync`
*Method Signature:* `ValueTask DisableMouseMoveAsync(ElementReference element)`


    Disables mouse move events for a specified HTML element asynchronously.
    



**Method:** `EnableMouseMoveAsync`
*Method Signature:* `ValueTask EnableMouseMoveAsync(ElementReference element, Object dotnetObjectReference)`


    Enables mouse move functionality for a specified element asynchronously.
    


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
    


