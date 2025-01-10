using CdCSharp.NjBlazor.Features.Markdown.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extension methods for configuring services in the service collection.</summary>
public static class MarkdownServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection to add NjBlazorMarkdown service with the specified lifetime.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the service to.</param>
    /// <param name="lifetime">The lifetime of the service (default is Transient).</param>
    public static void AddNjBlazorMarkdown(
        this IServiceCollection services,
        NjMarkdownSettings? settings = null,
        ServiceLifetime lifetime = ServiceLifetime.Transient) => settings ??= new NjMarkdownSettings();//services.AddNjBlazorCssInclude(settings.CssIncludeSettings, nameof(Nj.Blazor.Markdown), lifetime: lifetime);
}