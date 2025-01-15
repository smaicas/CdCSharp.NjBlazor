using CdCSharp.NjBlazor.Features.Forms.Checkbox.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsCheckboxServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorFormsCheckbox service.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsCheckbox(
        this IServiceCollection services,
        NjFormsCheckboxSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsCheckboxSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Checkbox), lifetime: lifetime);
}