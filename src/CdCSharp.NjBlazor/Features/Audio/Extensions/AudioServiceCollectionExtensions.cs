using CdCSharp.NjBlazor.Features.Audio.Abstractions;
using Nj.Blazor.Audio.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for configuring services in the service collection.
/// </summary>
public static class AudioServiceCollectionExtensions
{
    /// <summary>
    /// Adds NJBlazorAudio services to the specified <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection" /> to add the services to.
    /// </param>
    /// <param name="lifetime">
    /// The lifetime of the service. Default is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    public static void AddNjBlazorAudio(
        this IServiceCollection services,
        NjAudioSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        settings ??= new NjAudioSettings();

        //services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.Audio), lifetime: lifetime);
        services.AddAudioJsInterop(lifetime);
    }

    private static void AddAudioJsInterop(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) => services.Add(new ServiceDescriptor(typeof(IAudioJsInterop), typeof(AudioJsInterop), lifetime));
}