# NjInputFileJsCallbacksRelay

*Namespace:* CdCSharp.NjBlazor.Features.Forms.File
*Assembly:* CdCSharp.NjBlazor
*Source:* NjInputFileJsCallbacksRelay.cs



    Represents a class that relays JavaScript callbacks for input file operations.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(INjInputFileJsCallbacks callbacks)`


    Initializes a new instance of the NjInputFileJsCallbacksRelay class.
    



**Property:** `DotNetReference` (Public)


    Gets the .NET reference as an IDisposable object.
    

*Property Type:* `IDisposable`
*Nullable:* False
*Attributes:* 


**Method:** `Dispose`
*Method Signature:* `Void Dispose()`


    Disposes the underlying DotNetReference object.
    



**Method:** `NotifyChange`
*Method Signature:* `Task NotifyChange( files)`


    Notifies a change in the browser files from the JavaScript side.
    


