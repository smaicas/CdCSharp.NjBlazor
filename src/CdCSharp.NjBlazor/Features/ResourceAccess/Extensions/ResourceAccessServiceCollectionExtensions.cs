using CdCSharp.NjBlazor.Core.Cache;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using CdCSharp.NjBlazor.Features.Dom.Services;
using CdCSharp.NjBlazor.Features.ResourceAccess.Abstractions;
using CdCSharp.NjBlazor.Features.ResourceAccess.Components.FileRenderer;
using CdCSharp.NjBlazor.Features.ResourceAccess.Services;
using Microsoft.AspNetCore.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static partial class ResourceAccessServiceCollectionExtensions
{
    /// <summary>
    /// Adds services required for accessing Blazor resources in NJ.
    /// </summary>
    /// <param name="services">The collection of services to add to.</param>
    /// <param name="lifetime">The lifetime of the services to add (default is Scoped).</param>
    public static void AddNjBlazorResourceAccess(
        this IServiceCollection services,
        NjResourceAccessSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        settings ??= new NjResourceAccessSettings();

        //services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.ResourceAccess), lifetime);
        services.AddDomJsInterop(lifetime);
        services.AddFileRendererCache(lifetime);
        services.AddEmbeddedResourceAccessor(lifetime);
    }

    private static void AddEmbeddedResourceAccessor(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        services.Add(new ServiceDescriptor(typeof(ICacheService<EmbeddedResourceAccessor, string>), typeof(InMemoryCacheService<EmbeddedResourceAccessor, string>), lifetime));
        services.Add(new ServiceDescriptor(typeof(IEmbeddedResourceAccessor), typeof(EmbeddedResourceAccessor), lifetime));
    }

    private static void AddFileRendererCache(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.Add(new ServiceDescriptor(typeof(ICacheService<NjFileRenderer, RenderFragment>), typeof(InMemoryCacheService<NjFileRenderer, RenderFragment>), lifetime));

    private static void AddDomJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IDOMJsInterop), typeof(DomJsInterop), lifetime));
}