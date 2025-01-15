using CdCSharp.NjBlazor.Features.Forms.Color.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsColorServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorFormsColor service.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsColor(
        this IServiceCollection services,
        NjFormsColorSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsColorSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Color), lifetime: lifetime);
}