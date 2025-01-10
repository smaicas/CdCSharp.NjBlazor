using CdCSharp.NjBlazor.Features.Antiforgery.Abstractions;

namespace CdCSharp.NjBlazor.Features.Antiforgery.Services;

/// <summary>
/// Represents a factory for creating HttpClient instances with antiforgery protection.
/// </summary>
public class AntiforgeryHttpClientFactory : IAntiforgeryHttpClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAntiforgeryJsInterop _njJs;

    /// <summary>
    /// Initializes a new instance of the AntiforgeryHttpClientFactory class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory to create HTTP clients.</param>
    /// <param name="njJs">The Antiforgery JavaScript interop service.</param>
    /// <exception cref="ArgumentNullException">Thrown when httpClientFactory or njJs is null.</exception>
    public AntiforgeryHttpClientFactory(IHttpClientFactory httpClientFactory, IAntiforgeryJsInterop njJs)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _njJs = njJs ?? throw new ArgumentNullException(nameof(njJs));
    }

    /// <summary>Creates an HttpClient with the specified client name and includes an anti-forgery token in the request headers.</summary>
    /// <param name="clientName">The name of the HttpClient to create (default is "authorizedClient").</param>
    /// <returns>An HttpClient configured with the anti-forgery token in the request headers.</returns>
    /// <exception cref="Exception">Thrown when there is an issue retrieving the anti-forgery token.</exception>
    public async Task<HttpClient> CreateClientAsync(string clientName = "authorizedClient")
    {
        string token = await _njJs.GetAntiForgeryTokenAsync();

        HttpClient client = _httpClientFactory.CreateClient(clientName);
        client.DefaultRequestHeaders.Add("X-XSRF-TOKEN", token);

        return client;
    }
}