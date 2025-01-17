# LocalizationJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.Localization.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* LocalizationJsInterop.cs



    Represents a JavaScript interop class for localization functionality.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for localization functionality.
    



**Method:** `GetCultureAsync`
*Method Signature:* `ValueTask GetCultureAsync()`


    Asynchronously retrieves the culture information based on the cookie culture value.
    



**Method:** `SetCultureAsync`
*Method Signature:* `ValueTask SetCultureAsync(CultureInfo culture)`


    Sets the culture asynchronously for localization purposes.
    


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
    


