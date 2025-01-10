using CdCSharp.NjBlazor.Features.TextPattern.Abstractions;
using CdCSharp.NjBlazor.Features.TextPattern.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class TextPatternServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorTextPattern service to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorTextPattern(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.AddTextPatternJsInterop(lifetime);

    private static void AddTextPatternJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(ITextPatternJsInterop), typeof(TextPatternJsInterop), lifetime));
}