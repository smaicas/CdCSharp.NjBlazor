# AntiforgeryHttpClientFactory

*Namespace:* CdCSharp.NjBlazor.Features.Antiforgery.Services
*Assembly:* CdCSharp.NjBlazor
*Source:* AntiforgeryHttpClientFactory.cs



    Represents a factory for creating HttpClient instances with antiforgery protection.
    
---

**Method:** `.ctor`
*Method Signature:* `Void .ctor(IHttpClientFactory httpClientFactory, IAntiforgeryJsInterop njJs)`


    Initializes a new instance of the AntiforgeryHttpClientFactory class.
    



**Method:** `CreateClientAsync`
*Method Signature:* `Task CreateClientAsync(String clientName)`

Creates an HttpClient with the specified client name and includes an anti-forgery token in the request headers.


