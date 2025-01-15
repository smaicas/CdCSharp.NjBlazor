using CdCSharp.NjBlazor.Features.Forms.Radio.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsRadioServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorFormsRadio service.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsRadio(
        this IServiceCollection services,
        NjFormsRadioSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsRadioSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Radio), lifetime: lifetime);
}