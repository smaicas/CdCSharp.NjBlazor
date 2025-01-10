using CdCSharp.NjBlazor.Features.Forms.Dropdown.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension methods for configuring services in the service collection.</summary>
public static class FormsDropdownServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorFormsDropdown service.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorFormsDropdown(
        this IServiceCollection services,
        NjFormsDropdownSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsDropdownSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Dropdown), lifetime: lifetime);
}