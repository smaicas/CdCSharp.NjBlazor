using CdCSharp.NjBlazor.Features.Draggable.Abstractions;
using CdCSharp.NjBlazor.Features.Draggable.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension methods for configuring services in the service collection.</summary>
public static class DraggableServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NJBlazorDraggable functionality.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the NJBlazorDraggable functionality to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorDraggable(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.AddDraggableJsInterop(lifetime);

    private static void AddDraggableJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IDraggableJsInterop), typeof(DraggableJsInterop), lifetime));
}