using CdCSharp.NjBlazor.Features.Forms.Number.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services.
/// </summary>
public static class FormsNumberServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorFormsNumber to the specified IServiceCollection with the specified lifetime.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the NjBlazorFormsNumber service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsNumber(
        this IServiceCollection services,
        NjFormsNumberSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsNumberSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Number), lifetime: lifetime);
}