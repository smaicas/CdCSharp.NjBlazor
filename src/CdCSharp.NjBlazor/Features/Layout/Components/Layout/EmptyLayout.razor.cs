using CdCSharp.NjBlazor.Features.ThemeMode.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Layout;

/// <summary>
/// Represents an empty layout component that can be used as a base for other layout components.
/// </summary>
public partial class EmptyLayout : LayoutComponentBase
{
    /// <summary>Gets or sets the JavaScript interop service for theming.</summary>
    /// <value>The JavaScript interop service for theming.</value>
    [Inject]
    IThemeJsInterop ThemeJs { get; set; } = default!;

    /// <summary>
    /// Method called after rendering the component asynchronously.
    /// </summary>
    /// <param name="firstRender">A boolean value indicating if this is the first render of the component.</param>
    /// <returns>An asynchronous Task.</returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ThemeJs!.InitializeAsync();
        }
    }
}
