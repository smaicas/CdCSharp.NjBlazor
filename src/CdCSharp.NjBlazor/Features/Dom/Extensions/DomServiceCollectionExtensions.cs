using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using CdCSharp.NjBlazor.Features.Dom.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for registering services in the service collection.
/// </summary>
public static class DomServiceCollectionExtensions
{
    /// <summary>
    /// Adds NJBlazor DOM services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="lifetime">The lifetime of the service. Default is <see cref="ServiceLifetime.Transient"/>.</param>
    public static void AddNjBlazorDom(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.AddDomJsInterop(lifetime);

    private static void AddDomJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IDOMJsInterop), typeof(DomJsInterop), lifetime));
}