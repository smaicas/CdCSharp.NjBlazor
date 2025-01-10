using System.Globalization;

namespace CdCSharp.NjBlazor.Features.Localization.Abstractions;

/// <summary>
/// Represents settings related to localization.
/// </summary>
public sealed record LocalizationSettings
{
    /// <summary>
    /// Initializes a new instance of the LocalizationSettings class.
    /// </summary>
    /// <remarks>
    /// This constructor sets up the supported cultures for localization.
    /// </remarks>
    public LocalizationSettings()
    {
        SupportedCultures.Add("en-US");
        SupportedCultures.Add("es-ES");
    }

    /// <summary>Gets or sets the list of supported cultures.</summary>
    /// <value>The list of supported cultures.</value>
    public List<string> SupportedCultures { get; set; } = [];

    /// <summary>
    /// Converts the supported cultures represented as strings to CultureInfo objects.
    /// </summary>
    /// <returns>An IEnumerable of CultureInfo objects representing the supported cultures.</returns>
    public IEnumerable<CultureInfo> SupportedCulturesAsCultureInfo() => SupportedCultures.Select(c => new CultureInfo(c));
}