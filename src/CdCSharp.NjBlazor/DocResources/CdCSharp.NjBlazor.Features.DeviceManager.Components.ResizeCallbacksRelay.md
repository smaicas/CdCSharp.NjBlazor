# ResizeCallbacksRelay

*Namespace:* CdCSharp.NjBlazor.Features.DeviceManager.Components
*Assembly:* CdCSharp.NjBlazor
*Source:* ResizeCallbacksRelay.cs



    Represents a class that relays resize callbacks.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IResizeJsCallback callbacks)`


    Initializes a new instance of the ResizeCallbacksRelay class.
    



**Property:** `DotNetReference` (Public)


    Gets the .NET reference as an IDisposable.
    

*Property Type:* `IDisposable`
*Nullable:* False
*Attributes:* 


**Method:** `Dispose`
*Method Signature:* `Void Dispose()`


    Disposes the underlying DotNetReference object.
    



**Method:** `NotifyResize`
*Method Signature:* `Task NotifyResize(Int32 windowWidth)`


    Notifies the callback function about a window resize event.
    


