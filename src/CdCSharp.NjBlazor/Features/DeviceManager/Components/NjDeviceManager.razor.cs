using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.DeviceManager.Abstractions;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.DeviceManager.Components;
/// <summary>
/// Represents a device manager for Nj devices.
/// </summary>
/// <remarks>
/// This class extends NjComponentBase and implements the IResizeJsCallback interface.
/// </remarks>
public partial class NjDeviceManager : NjComponentBase, IResizeJsCallback
{
    /// <summary>
    /// Gets or sets the JavaScript interop service for device management.
    /// </summary>
    [Inject]
    IDeviceManagerJsInterop DeviceJs { get; set; } = default!;

    private int _deviceWidth;

    /// <summary>Gets or sets the device width.</summary>
    /// <value>The device width.</value>
    [Parameter]
    public int DeviceWidth
    {
        get => _deviceWidth;
        set
        {
            if (_deviceWidth == value)
                return;
            _deviceWidth = value;
            DeviceWidthChanged.InvokeAsync(_deviceWidth);
            StateHasChanged();
        }
    }

    /// <summary>Gets or sets the event callback for device width changes.</summary>
    /// <value>The event callback for device width changes.</value>
    [Parameter]
    public EventCallback<int> DeviceWidthChanged { get; set; }

    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private ResizeCallbacksRelay? _jsCallbacksRelay;

    /// <summary>
    /// Retrieves a value based on the device width category.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to retrieve.</typeparam>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices.</param>
    /// <param name="valueDesktop">The value for desktop devices.</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices.</param>
    /// <returns>The retrieved value based on the device width category.</returns>
    /// <remarks>
    /// If the device width is not set, returns null.
    /// </remarks>
    public TValue? GetByWidth<TValue>(
        TValue? valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        if (DeviceWidth == default)
            return null;

        return DeviceHelper.GetByWidth(
            DeviceWidth,
            valueMobile,
            valueTablet,
            valueDesktop,
            valueLargeDesktop
        );
    }

    /// <summary>
    /// Gets the appropriate value based on the device width.
    /// </summary>
    /// <typeparam name="TValue">The type of value to return.</typeparam>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices (optional).</param>
    /// <param name="valueDesktop">The value for desktop devices (optional).</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices (optional).</param>
    /// <returns>The appropriate value based on the device width.</returns>
    /// <typeparam name="TValue">The type of value to return.</typeparam>
    public TValue GetByWidth<TValue>(
        TValue valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : struct
    {
        if (DeviceWidth == default)
            return valueMobile;
        return DeviceHelper.GetByWidth(
            DeviceWidth,
            valueMobile,
            valueTablet,
            valueDesktop,
            valueLargeDesktop
        );
    }

    /// <summary>
    /// Retrieves a value based on the device width category.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to retrieve.</typeparam>
    /// <param name="valueMobile">The value for mobile devices.</param>
    /// <param name="valueTablet">The value for tablet devices.</param>
    /// <param name="valueDesktop">The value for desktop devices.</param>
    /// <param name="valueLargeDesktop">The value for large desktop devices.</param>
    /// <returns>The retrieved value based on the device width category.</returns>
    /// <remarks>
    /// If the device width is not set, returns null.
    /// </remarks>
    public TValue? GetByWidth<TValue>(
        TValue valueMobile,
        TValue? valueTablet = null,
        TValue? valueDesktop = null,
        TValue? valueLargeDesktop = null
    )
        where TValue : class
    {
        if (DeviceWidth == default)
            return null;

        return DeviceHelper.GetByWidth(
            DeviceWidth,
            valueMobile,
            valueTablet,
            valueDesktop,
            valueLargeDesktop
        );
    }

    /// <summary>
    /// Method called after rendering to perform additional operations if it's the first render.
    /// </summary>
    /// <param name="firstRender">A boolean value indicating if it's the first render.</param>
    /// <returns>An asynchronous task.</returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsCallbacksRelay = new ResizeCallbacksRelay(this);
            await DeviceJs.AddResizeCallback(
                _jsCallbacksRelay.DotNetReference,
                nameof(_jsCallbacksRelay.NotifyResize)
            );
            DeviceWidth = await DeviceJs.GetWindowWidth();
        }
    }

    /// <summary>
    /// Notifies the object about a window resize event.
    /// </summary>
    /// <param name="windowWidth">The new width of the window.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task NotifyResize(int windowWidth)
    {
        DeviceWidth = windowWidth;
        return Task.CompletedTask;
    }
}
