using CdCSharp.NjBlazor.Features.Antiforgery.Abstractions;
using CdCSharp.NjBlazor.Features.Antiforgery.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class AntiForgeryServiceCollectionExtensions
{
    /// <summary>
    /// Adds NJ Antiforgery services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="lifetime">The lifetime of the service. Default is <see cref="ServiceLifetime.Transient"/>.</param>
    public static void AddNjAntiforgery(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.AddAntiforgeryJsInterop(lifetime);

    private static void AddAntiforgeryJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.Add(new ServiceDescriptor(typeof(IAntiforgeryJsInterop), typeof(AntiforgeryJsInterop), lifetime));

}