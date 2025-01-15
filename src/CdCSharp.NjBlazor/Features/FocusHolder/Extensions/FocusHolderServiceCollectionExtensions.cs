namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FocusHolderServiceCollectionExtensions
{
    /// <summary>
    /// Adds a Blazor focus holder service to the specified service collection.
    /// </summary>
    /// <param name="services">
    /// The collection of services to add the focus holder service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFocusHolder(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
    { }
}