# FocusHolderJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.FocusHolder.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* FocusHolderJsInterop.cs



    Represents a JavaScript interop class for managing focus in Blazor applications.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for managing focus in Blazor applications.
    



**Method:** `FocusElementAsync`
*Method Signature:* `ValueTask FocusElementAsync(String querySelector, Nullable parentElement)`


    Asynchronously focuses on an element specified by the query selector.
    


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
    


