using CdCSharp.NjBlazor.Features.Media.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension methods for configuring services in the service collection.</summary>
public static class MediaServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorMedia service with the specified lifetime.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorMedia(
        this IServiceCollection services,
        NjMediaSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjMediaSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.Media), lifetime: lifetime);
}