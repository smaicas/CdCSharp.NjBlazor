# DeviceManagerJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.DeviceManager.Components
*Assembly:* CdCSharp.NjBlazor
*Source:* DeviceManagerJsInterop.cs



    Represents a JavaScript interop class for managing devices.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for managing devices.
    



**Method:** `GetWindowWidth`
*Method Signature:* `ValueTask GetWindowWidth()`


    Asynchronously retrieves the width of the window.
    



**Method:** `GetByWidth`
*Method Signature:* `ValueTask GetByWidth(TValue valueMobile, Nullable valueTablet, Nullable valueDesktop, Nullable valueLargeDesktop)`


    Retrieves a value based on the width of the device.
    



**Method:** `GetByWidth`
*Method Signature:* `ValueTask GetByWidth(Nullable valueMobile, Nullable valueTablet, Nullable valueDesktop, Nullable valueLargeDesktop)`


    Retrieves a value based on the width of the device.
    



**Method:** `AddResizeCallback`
*Method Signature:* `ValueTask AddResizeCallback(Object dotnetReference, String callbackName)`


    Adds a window resize event callback for a specified dotnet reference and callback name.
    


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
    


