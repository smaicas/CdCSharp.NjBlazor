using System.Globalization;

namespace CdCSharp.NjBlazor.Features.Localization.Abstractions;

/// <summary>Interface for JavaScript interop related to localization.</summary>
public interface ILocalizationJsInterop
{
    /// <summary>
    /// Asynchronously retrieves the current culture information.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation. The task result contains the <see cref="CultureInfo"/> object representing the current culture, or null if not available.</returns>
    ValueTask<CultureInfo?> GetCultureAsync();

    /// <summary>Sets the culture asynchronously.</summary>
    /// <param name="culture">The culture to set.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SetCultureAsync(CultureInfo culture);
}