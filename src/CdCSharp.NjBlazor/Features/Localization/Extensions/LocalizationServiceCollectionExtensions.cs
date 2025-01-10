using CdCSharp.NjBlazor.Features.Localization.Abstractions;
using CdCSharp.NjBlazor.Features.Localization.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static partial class LocalizationServiceCollectionExtensions
{
    /// <summary>
    /// Adds NJ Blazor localization services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="localizationSettings">The localization settings to configure the localization services.</param>
    /// <param name="lifetime">The lifetime of the service. Default is <see cref="ServiceLifetime.Transient"/>.</param>
    public static void AddNjBlazorLocalization(this IServiceCollection services, LocalizationSettings localizationSettings, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        services.AddLocalizationServices(localizationSettings);
        services.AddLocalizationJsInterop(lifetime);
    }

    private static void AddLocalizationJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(ILocalizationJsInterop), typeof(LocalizationJsInterop), lifetime));

    private static void AddLocalizationServices(this IServiceCollection services, LocalizationSettings localizationSettings)
    {
        services.Configure<LocalizationSettings>(options => options.SupportedCultures = localizationSettings.SupportedCultures);
        services.AddLocalization(options => options.ResourcesPath = "Resources");
    }
}