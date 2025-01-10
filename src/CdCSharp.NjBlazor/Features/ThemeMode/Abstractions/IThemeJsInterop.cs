namespace CdCSharp.NjBlazor.Features.ThemeMode.Abstractions;

/// <summary>Represents an interface for interacting with themes using JavaScript interop.</summary>
public interface IThemeJsInterop
{
    /// <summary>Initializes the asynchronous task.</summary>
    ValueTask InitializeAsync();

    /// <summary>Sets the dark mode asynchronously.</summary>
    /// <param name="isDarkMode">A boolean indicating whether to set dark mode.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SetDarkModeAsync(bool isDarkMode);

    /// <summary>
    /// Toggles the dark mode setting.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation with a boolean indicating the new state of dark mode.</returns>
    ValueTask<bool> ToggleDarkMode();

    /// <summary>
    /// Checks if the application is in dark mode.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing whether the application is in dark mode (true) or not (false).</returns>
    ValueTask<bool> IsDarkMode();
}
