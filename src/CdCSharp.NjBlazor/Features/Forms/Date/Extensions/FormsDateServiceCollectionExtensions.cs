using CdCSharp.NjBlazor.Features.Forms.Date.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsDateServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorFormsDate service.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsDate(
        this IServiceCollection services,
        NjFormsDateSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsDateSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Date), lifetime: lifetime);
}