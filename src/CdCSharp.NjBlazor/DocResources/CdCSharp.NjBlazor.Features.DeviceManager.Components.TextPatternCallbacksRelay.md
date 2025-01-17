# TextPatternCallbacksRelay

*Namespace:* CdCSharp.NjBlazor.Features.DeviceManager.Components
*Assembly:* CdCSharp.NjBlazor
*Source:* TextPatternCallbacksRelay.cs



    Represents a class that relays resize callbacks.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(ITextPatternJsCallback callbacks)`


    Initializes a new instance of the ResizeCallbacksRelay class.
    



**Property:** `DotNetReference` (Public)


    Gets the .NET reference as an IDisposable.
    

*Property Type:* `IDisposable`
*Nullable:* False
*Attributes:* 


**Method:** `Dispose`
*Method Signature:* `Void Dispose()`


    Disposes the underlying DotNetReference object.
    



**Method:** `NotifyTextChanged`
*Method Signature:* `Task NotifyTextChanged(String text)`


    Notifies the callback function about a window resize event.
    



**Method:** `ValidatePartial`
*Method Signature:* `Task ValidatePartial(Int32 index, String text)`


    Validates partial pattern.
    


