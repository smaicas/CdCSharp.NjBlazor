namespace CdCSharp.NjBlazor.Features.Antiforgery.Abstractions;

/// <summary>
/// Interface for a factory that creates HttpClient instances with antiforgery token support.
/// </summary>
public interface IAntiforgeryHttpClientFactory
{
    /// <summary>
    /// Creates an instance of HttpClient asynchronously with the specified client name.
    /// </summary>
    /// <param name="clientName">The name of the HttpClient instance to create (default is "authorizedClient").</param>
    /// <returns>A task representing the asynchronous operation that returns the created HttpClient instance.</returns>
    Task<HttpClient> CreateClientAsync(string clientName = "authorizedClient");
}