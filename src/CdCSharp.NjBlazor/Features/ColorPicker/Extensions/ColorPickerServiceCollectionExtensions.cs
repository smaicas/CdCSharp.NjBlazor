using CdCSharp.NjBlazor.Features.ColorPicker.Abstractions;
using CdCSharp.NjBlazor.Features.ColorPicker.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class ColorPickerServiceCollectionExtensions
{
    /// <summary>
    /// Adds the NjBlazorColorPicker service to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">
    /// The IServiceCollection to add the service to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service (default is Transient).
    /// </param>
    public static void AddNjBlazorColorPicker(
        this IServiceCollection services,
        NjColorPickerSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        settings ??= new NjColorPickerSettings();

        //services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.ColorPicker), lifetime: lifetime);
        services.AddColorPickerJsInterop(lifetime);
    }

    private static void AddColorPickerJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IColorPickerJsInterop), typeof(ColorPickerJsInterop), lifetime));
}