using CdCSharp.NjBlazor.Features.Forms.File.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class FormsFileServiceCollectionExtensions
{
    /// <summary>
    /// Adds NjBlazorFormsFile to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the NjBlazorFormsFile to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorFormsFile(
        this IServiceCollection services,
        NjFormsFileSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjFormsFileSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(CdCSharp.NjBlazor.Features.Forms.File), lifetime: lifetime);
}