using CdCSharp.NjBlazor.Features.DeviceManager.Abstractions;
using CdCSharp.NjBlazor.Features.DeviceManager.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class DeviceManagerServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorDeviceManager service using DeviceManagerJsInterop.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorDeviceManager(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.AddDeviceManagerJsInterop(lifetime);

    private static void AddDeviceManagerJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IDeviceManagerJsInterop), typeof(DeviceManagerJsInterop), lifetime));
}