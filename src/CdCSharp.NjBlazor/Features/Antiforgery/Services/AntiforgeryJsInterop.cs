using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Antiforgery.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.Antiforgery.Services;

/// <summary>
/// Represents a JavaScript interop class for Antiforgery functionality.
/// </summary>
/// <param name="jsRuntime">
/// The JavaScript runtime instance.
/// </param>
/// <seealso cref="ModuleJsInterop" />
/// <seealso cref="IAntiforgeryJsInterop" />
public class AntiforgeryJsInterop(IJSRuntime jsRuntime) : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.AntiforgeryJs), IAntiforgeryJsInterop
{
    /// <summary>
    /// Asynchronously retrieves the anti-forgery token.
    /// </summary>
    /// <returns>
    /// The anti-forgery token as a string.
    /// </returns>
    public async ValueTask<string> GetAntiForgeryTokenAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<string>("AntiforgeryJs.GetAntiForgeryToken");
    }

    /// <summary>
    /// Asynchronously retrieves a nonce value from the JavaScript runtime.
    /// </summary>
    /// <returns>
    /// A string representing the nonce value.
    /// </returns>
    public async ValueTask<string> GetNonceAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<string>("AntiforgeryJs.GetNonce");
    }
}