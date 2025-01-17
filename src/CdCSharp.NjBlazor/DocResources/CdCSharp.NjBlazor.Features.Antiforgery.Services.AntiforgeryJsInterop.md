# AntiforgeryJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.Antiforgery.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* AntiforgeryJsInterop.cs



    Represents a JavaScript interop class for Antiforgery functionality.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for Antiforgery functionality.
    



**Method:** `GetAntiForgeryTokenAsync`
*Method Signature:* `ValueTask GetAntiForgeryTokenAsync()`


    Asynchronously retrieves the anti-forgery token.
    



**Method:** `GetNonceAsync`
*Method Signature:* `ValueTask GetNonceAsync()`


    Asynchronously retrieves a nonce value from the JavaScript runtime.
    


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
    


