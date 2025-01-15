using CdCSharp.NjBlazor.Features.Forms.Range.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsRangeServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorFormsRange service with the specified lifetime.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsRange(
        this IServiceCollection services,
        NjFormsRangeSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsRangeSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Range), lifetime: lifetime);
}