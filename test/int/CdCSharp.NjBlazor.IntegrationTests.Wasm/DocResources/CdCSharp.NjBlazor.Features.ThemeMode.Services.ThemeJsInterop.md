# ThemeJsInterop

*Namespace:* CdCSharp.NjBlazor.Features.ThemeMode.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* ThemeJsInterop.cs



    Represents a JavaScript interop class for handling theme-related operations.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IJSRuntime jsRuntime)`


    Represents a JavaScript interop class for handling theme-related operations.
    



**Method:** `InitializeAsync`
*Method Signature:* `ValueTask InitializeAsync()`


    Asynchronously initializes the module.
    



**Method:** `IsDarkMode`
*Method Signature:* `ValueTask IsDarkMode()`


    Asynchronously checks if the application is in dark mode.
    



**Method:** `SetDarkModeAsync`
*Method Signature:* `ValueTask SetDarkModeAsync(Boolean isDarkMode)`


    Sets the dark mode asynchronously.
    



**Method:** `ToggleDarkMode`
*Method Signature:* `ValueTask ToggleDarkMode()`


    Toggles the dark mode using JavaScript function 'ThemeJs.ToggleDarkMode'.
    


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
    


