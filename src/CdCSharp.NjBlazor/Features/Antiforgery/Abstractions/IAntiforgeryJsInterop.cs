namespace CdCSharp.NjBlazor.Features.Antiforgery.Abstractions;

/// <summary>
/// Interface for interacting with Antiforgery features in JavaScript.
/// </summary>
public interface IAntiforgeryJsInterop
{
    /// <summary>
    /// Asynchronously retrieves an anti-forgery token.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation with the anti-forgery token as a string.</returns>
    ValueTask<string> GetAntiForgeryTokenAsync();

    /// <summary>
    /// Asynchronously retrieves a nonce.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation, containing the nonce as a string.</returns>
    ValueTask<string> GetNonceAsync();
}