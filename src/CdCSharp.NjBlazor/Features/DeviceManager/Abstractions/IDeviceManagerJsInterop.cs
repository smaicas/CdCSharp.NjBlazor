namespace CdCSharp.NjBlazor.Features.DeviceManager.Abstractions;

/// <summary>
/// Interface for interacting with device manager through JavaScript interop.
/// </summary>
public interface IDeviceManagerJsInterop
{
    /// <summary>Gets the width of the window.</summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the width of the window.</returns>
    ValueTask<int> GetWindowWidth();

    /// <summary>
    /// Gets the value based on the width category.
    /// </summary>
    /// <typeparam name="TValue">The type of value to retrieve.</typeparam>
    /// <param name="valueMobile">The value for mobile width.</param>
    /// <param name="valueTablet">The value for tablet width (optional).</param>
    /// <param name="valueDesktop">The value for desktop width (optional).</param>
    /// <param name="valueLargeDesktop">The value for large desktop width (optional).</param>
    /// <returns>The value based on the width category.</returns>
    /// <typeparam name="TValue">The type of value to retrieve.</typeparam>
    ValueTask<TValue> GetByWidth<TValue>(
        TValue valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct;

    /// <summary>
    /// Gets the value based on the width category.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="valueMobile">The value for mobile width.</param>
    /// <param name="valueTablet">The value for tablet width.</param>
    /// <param name="valueDesktop">The value for desktop width.</param>
    /// <param name="valueLargeDesktop">The value for large desktop width.</param>
    /// <returns>The value based on the width category.</returns>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    ValueTask<TValue?> GetByWidth<TValue>(
        TValue? valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct;

    /// <summary>
    /// Adds a window resize event callback for a specified .NET reference using the provided callback name.
    /// </summary>
    /// <param name="dotnetReference">The .NET reference to associate with the resize callback.</param>
    /// <param name="callbackName">The name of the callback function to be invoked on resize.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask AddResizeCallback(object dotnetReference, string callbackName);
}
