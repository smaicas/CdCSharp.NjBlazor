using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Media.Components;
using CdCSharp.NjBlazor.Features.ThemeMode.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.ThemeMode.Components.ModeSelector;

/// <summary>
/// Base class for a theme mode selector component.
/// </summary>
/// <remarks>
/// This class serves as the base for components that allow users to select a theme mode.
/// </remarks>
public abstract class NjThemeModeSelectorBase : NjComponentBase
{
    /// <summary>
    /// Indicates whether the application is in dark mode.
    /// </summary>
    protected bool IsDarkMode;

    /// <summary>
    /// Gets or sets the size of the SVG icon.
    /// </summary>
    /// <value>
    /// The size of the SVG icon.
    /// </value>
    [Parameter]
    public NjSvgIconSize IconSize { get; set; } = NjSvgIconSize.Medium;

    /// <summary>
    /// Gets or sets the JavaScript interop service for theming.
    /// </summary>
    /// <value>
    /// The JavaScript interop service for theming.
    /// </value>
    [Inject]
    public IThemeJsInterop? ThemeJs { get; set; }

    /// <summary>
    /// Gets the CSS class for dark mode based on the IsDarkMode property.
    /// </summary>
    /// <value>
    /// The CSS class for dark mode if IsDarkMode is true; otherwise, an empty string.
    /// </value>
    protected string DarkClass => IsDarkMode ? "darkMode" : "";

    /// <summary>
    /// Toggles the dark mode using the provided ThemeJs object.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the ThemeJs object is null.
    /// </exception>
    protected async Task ToggleDarkMode()
    {
        if (ThemeJs == null) throw new ArgumentNullException(nameof(ThemeJs));
        IsDarkMode = await ThemeJs.ToggleDarkMode();
    }
}