using CdCSharp.NjBlazor.Features.ThemeMode.Abstractions;
using CdCSharp.NjBlazor.Features.ThemeMode.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class ThemeModeServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazor theme mode to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the NjBlazor theme mode to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorThemeMode(
        this IServiceCollection services,
        NjThemeModeSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        settings ??= new NjThemeModeSettings();

        //services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.ThemeMode), lifetime);
        services.AddThemeJsInterop(lifetime);
    }

    private static void AddThemeJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IThemeJsInterop), typeof(ThemeJsInterop), lifetime));
}