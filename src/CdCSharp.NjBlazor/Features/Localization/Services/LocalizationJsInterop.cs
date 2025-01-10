using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Localization.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net;

namespace CdCSharp.NjBlazor.Features.Localization.Services;

/// <summary>
/// Represents a JavaScript interop class for localization functionality.
/// </summary>
/// <param name="jsRuntime">The JavaScript runtime instance.</param>
/// <seealso cref="ModuleJsInterop"/>
/// <seealso cref="ILocalizationJsInterop"/>
public class LocalizationJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.LocalizationJs)
    , ILocalizationJsInterop
{
    /// <summary>
    /// Asynchronously retrieves the culture information based on the cookie culture value.
    /// </summary>
    /// <returns>A <see cref="CultureInfo"/> object representing the culture information, or null if not found.</returns>
    public async ValueTask<CultureInfo?> GetCultureAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        CultureInfo? result = null;
        string? cookieCulture = await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.Get);
        if (cookieCulture != null)
        {
            string? cultureName = WebUtility.UrlDecode(cookieCulture)?.Split("|")[0].Split("=")[1];
            result = cultureName != null ? new CultureInfo(cultureName) : null;
        }

        return result;
    }

    /// <summary>
    /// Sets the culture asynchronously for localization purposes.
    /// </summary>
    /// <param name="culture">The CultureInfo object representing the culture to set.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    public async ValueTask SetCultureAsync(CultureInfo culture)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        string cultureCookieValue = WebUtility.UrlEncode($"c={culture.Name}|uic={culture.Name}");

        await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.Set, cultureCookieValue);
    }
}