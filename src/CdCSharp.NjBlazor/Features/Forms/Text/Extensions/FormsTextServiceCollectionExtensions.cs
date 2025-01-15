using CdCSharp.NjBlazor.Features.Forms.Text.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsTextServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorFormsText to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the NjBlazorFormsText to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsText(
        this IServiceCollection services,
        NjFormsTextSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsTextSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.Text), lifetime: lifetime);
}