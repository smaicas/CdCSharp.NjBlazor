
using CdCSharp.NjBlazor.Features.Layout.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension methods for configuring services in the service collection.</summary>
public static class LayoutServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorLayout to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the NjBlazorLayout to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorLayout(
        this IServiceCollection services,
        NjLayoutSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        settings ??= new NjLayoutSettings();

        //services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.Layout), lifetime: lifetime);
    }
}