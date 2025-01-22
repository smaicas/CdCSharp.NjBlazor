using CdCSharp.NjBlazor.Features.LocalStorage.Abstractions;
using CdCSharp.NjBlazor.Features.LocalStorage.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CdCSharp.NjBlazor.Features.Tree.Extensions;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class LocalStorageServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorLocalStorage services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection" /> to add the services to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service. Default is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    public static void AddNjBlazorLocalStorage(
        this IServiceCollection services,
        NjLocalStorageSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        settings ??= new NjLocalStorageSettings();

        services.AddLocalStorageJsInterop(lifetime);
    }

    private static void AddLocalStorageJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.Add(new ServiceDescriptor(typeof(ILocalStorageJsInterop), typeof(LocalStorageJsInterop), lifetime));
}