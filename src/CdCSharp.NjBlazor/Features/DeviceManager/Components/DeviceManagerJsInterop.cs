using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.DeviceManager.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.DeviceManager.Components;

/// <summary>
/// Represents a JavaScript interop class for managing devices.
/// </summary>
/// <param name="jsRuntime">
/// The JavaScript runtime instance.
/// </param>
/// <seealso cref="ModuleJsInterop" />
/// <seealso cref="IDeviceManagerJsInterop" />
public class DeviceManagerJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.DeviceManagerJs),
        IDeviceManagerJsInterop
{
    /// <summary>
    /// Adds a window resize event callback for a specified dotnet reference and callback name.
    /// </summary>
    /// <param name="dotnetReference">
    /// The dotnet reference to associate with the callback.
    /// </param>
    /// <param name="callbackName">
    /// The name of the callback function.
    /// </param>
    /// <returns>
    /// A <see cref="ValueTask" /> representing the asynchronous operation.
    /// </returns>
    public async ValueTask AddResizeCallback(object dotnetReference, string callbackName)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(
            CSharpReferences.Functions.AddResizeCallback,
            dotnetReference,
            callbackName
        );
    }

    /// <summary>
    /// Retrieves a value based on the width of the device.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of value to retrieve.
    /// </typeparam>
    /// <param name="valueMobile">
    /// The value for mobile devices.
    /// </param>
    /// <param name="valueTablet">
    /// The value for tablet devices (optional).
    /// </param>
    /// <param name="valueDesktop">
    /// The value for desktop devices (optional).
    /// </param>
    /// <param name="valueLargeDesktop">
    /// The value for large desktop devices (optional).
    /// </param>
    /// <returns>
    /// The retrieved value based on the device's width.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the module task is not loaded.
    /// </exception>
    public async ValueTask<TValue> GetByWidth<TValue>(
        TValue valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        int deviceWidth = await JsRuntime.InvokeAsync<int>(
            CSharpReferences.Functions.GetWindowWidth
        );

        return DeviceHelper.GetByWidth(
            deviceWidth,
            valueMobile,
            valueTablet,
            valueDesktop,
            valueLargeDesktop
        );
    }

    /// <summary>
    /// Retrieves a value based on the width of the device.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value to retrieve.
    /// </typeparam>
    /// <param name="valueMobile">
    /// The value for mobile devices.
    /// </param>
    /// <param name="valueTablet">
    /// The value for tablet devices.
    /// </param>
    /// <param name="valueDesktop">
    /// The value for desktop devices.
    /// </param>
    /// <param name="valueLargeDesktop">
    /// The value for large desktop devices.
    /// </param>
    /// <returns>
    /// The retrieved value based on the width of the device.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the module task is not loaded.
    /// </exception>
    public async ValueTask<TValue?> GetByWidth<TValue>(
        TValue? valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        int deviceWidth = await JsRuntime.InvokeAsync<int>(
            CSharpReferences.Functions.GetWindowWidth
        );

        return DeviceHelper.GetByWidth(
            deviceWidth,
            valueMobile,
            valueTablet,
            valueDesktop,
            valueLargeDesktop
        );
    }

    /// <summary>
    /// Asynchronously retrieves the width of the window.
    /// </summary>
    /// <returns>
    /// The width of the window as an integer.
    /// </returns>
    public async ValueTask<int> GetWindowWidth()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<int>(CSharpReferences.Functions.GetWindowWidth);
    }
}