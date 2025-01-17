# TextPatternJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.TextPattern.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* TextPatternJsInterop.cs



    Represents a JavaScript interop class for text pattern operations.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for text pattern operations.
    



**Method:** `TextPatternAddDynamic`
*Method Signature:* `ValueTask TextPatternAddDynamic(ElementReference contentBox, IEnumerable elements, Object dotnetReference, String notifyChangedTextCallback, String validatePartialCallback)`


    Adds dynamic text patterns to the specified content box using the provided elements.
    


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
    


