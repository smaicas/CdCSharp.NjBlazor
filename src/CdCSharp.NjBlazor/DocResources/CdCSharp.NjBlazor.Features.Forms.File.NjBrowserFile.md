# NjBrowserFile

*Namespace:* CdCSharp.NjBlazor.Features.Forms.File
*Assembly:* CdCSharp.NjBlazor
*Source:* NjBrowserFile.cs



    Represents a file uploaded via a browser.
    
---

**Property:** `ContentType` (Public)


    Gets or sets the content type.
    

*Property Type:* `String`
*Default:* `string.Empty`
*Nullable:* False
*Attributes:* 


**Property:** `Id` (Public)


    Gets or sets the identifier.
    

*Property Type:* `Int32`
*Nullable:* False
*Attributes:* 


**Property:** `LastModified` (Public)


    Gets or sets the date and time when the object was last modified.
    

*Property Type:* `DateTimeOffset`
*Nullable:* False
*Attributes:* 


**Property:** `Name` (Public)


    Gets or sets the name.
    

*Property Type:* `String`
*Default:* `string.Empty`
*Nullable:* False
*Attributes:* 


**Property:** `RelativePath` (Public)


    Gets or sets the relative path.
    

*Property Type:* `String`
*Nullable:* True
*Attributes:* 


**Property:** `Size` (Public)


    Gets or sets the size of an object.
    

*Property Type:* `Int64`
*Nullable:* False
*Attributes:* 


**Property:** `Owner` (Internal)


    Gets or sets the owner of the input file.
    

*Property Type:* `NjInputFileBase`
*Nullable:* False
*Attributes:* 


**Method:** `OpenReadStream`
*Method Signature:* `Stream OpenReadStream(Int64 maxAllowedSize, CancellationToken cancellationToken)`


    Opens a read stream for the file.
    



**Method:** `.ctor`
*Method Signature:* `Void .ctor()`

