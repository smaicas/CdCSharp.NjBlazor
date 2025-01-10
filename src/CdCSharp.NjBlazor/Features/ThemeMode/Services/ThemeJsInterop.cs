using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.ThemeMode.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.ThemeMode.Services;

/// <summary>
/// Represents a JavaScript interop class for handling theme-related operations.
/// </summary>
/// <param name="jsRuntime">The JavaScript runtime instance.</param>
/// <seealso cref="ModuleJsInterop"/>
/// <seealso cref="IThemeJsInterop"/>
public class ThemeJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.ThemeJs), IThemeJsInterop
{
    /// <summary>
    /// Asynchronously initializes the module.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    public async ValueTask InitializeAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.ThemeJsInitializeAsync);
    }

    /// <summary>
    /// Sets the dark mode asynchronously.
    /// </summary>
    /// <param name="isDarkMode">A boolean value indicating whether to set dark mode.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    public async ValueTask SetDarkModeAsync(bool isDarkMode)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.ThemeJsSetDarkMode, isDarkMode);
    }

    /// <summary>
    /// Toggles the dark mode using JavaScript function 'ThemeJs.ToggleDarkMode'.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation with a boolean indicating the success of toggling dark mode.</returns>
    public async ValueTask<bool> ToggleDarkMode()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<bool>(CSharpReferences.Functions.ThemeJsToggleDarkMode);
    }

    /// <summary>
    /// Asynchronously checks if the application is in dark mode.
    /// </summary>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the task result, where true indicates dark mode and false indicates light mode.</returns>
    public async ValueTask<bool> IsDarkMode()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<bool>(CSharpReferences.Functions.ThemeJsIsDarkMode);
    }
}