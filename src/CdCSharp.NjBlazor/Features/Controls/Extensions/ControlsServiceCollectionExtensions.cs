using CdCSharp.NjBlazor.Features.Controls.Abstractions;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using CdCSharp.NjBlazor.Features.Dom.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Microsoft.Extensions.DependencyInjection Service Collection Extensions
/// </summary>
public static partial class ControlsServiceCollectionExtensions
{
    public static void AddNjBlazorControls(
        this IServiceCollection services,
        NjControlsSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        settings ??= new NjControlsSettings();

        //services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.Controls), lifetime: lifetime);
        services.AddDomJsInterop(lifetime);
    }

    private static void AddDomJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) =>
        services.Add(new ServiceDescriptor(typeof(IDOMJsInterop), typeof(DomJsInterop), lifetime));
}