using CdCSharp.NjBlazor.Core.Cache;
using CdCSharp.NjBlazor.Features.Layout.Components.Tree;
using CdCSharp.NjBlazor.Features.Tree.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CdCSharp.NjBlazor.Features.Tree.Extensions;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class TreeServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorTree services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection" /> to add the services to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service. Default is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    public static void AddNjBlazorTree(
        this IServiceCollection services,
        NjTreeSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        settings ??= new NjTreeSettings();

        services.AddNjTreeCache(ServiceLifetime.Transient);
    }

    private static void AddNjTreeMemoryManager(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        //services.Add(new ServiceDescriptor(typeof(INjTreeMemoryManager), typeof(NjTreeLocalStorageMemoryManager), lifetime));
    }
    private static void AddNjTreeCache(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.Add(new ServiceDescriptor(typeof(ICacheService<NjTree, bool>), typeof(LocalStorageCacheService<NjTree, bool>), lifetime));
}