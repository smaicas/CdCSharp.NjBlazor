using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.DeviceManager.Abstractions;
using CdCSharp.NjBlazor.Features.DeviceManager.Components;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Spacer;

/// <summary>
/// Represents a spacer component in the Nj framework.
/// This component is used for creating empty space in layouts.
/// </summary>
public partial class NjSpacer : NjComponentBase, IResizeJsCallback
{
    /// <summary>
    /// Gets or sets the DOM JavaScript interop service for interacting with the Document Object Model (DOM) in JavaScript.
    /// </summary>
    /// <remarks>
    /// The DOMJsInterop service provides methods for performing JavaScript interop operations on the DOM.
    /// </remarks>
    [Inject]
    protected IDOMJsInterop DomJs { get; set; } = default!;

    /// <summary>
    /// Gets or sets the JavaScript interop service for device management.
    /// </summary>
    [Inject]
    IDeviceManagerJsInterop DeviceJs { get; set; } = default!;

    /// <summary>Gets or sets the vertical position.</summary>
    /// <value>The vertical position.</value>
    [Parameter]
    public int? Vertical { get; set; }

    /// <summary>Gets or sets the horizontal value.</summary>
    /// <value>The horizontal value.</value>
    [Parameter]
    public int? Horizontal { get; set; }

    /// <summary>
    /// Query element to add vertical spacing relative to.
    /// </summary>
    [Parameter]
    public string? VerticalRelativeToQuery { get; set; }

    /// <summary>
    /// Query element to add horizontal spacing relative to.
    /// </summary>
    [Parameter]
    public string? HorizontalRelativeToQuery { get; set; }

    private string InlineStyle = string.Empty;

    protected override async Task OnInitializedAsync() => await base.OnInitializedAsync();
    private ResizeCallbacksRelay? _jsCallbacksRelay;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsCallbacksRelay = new ResizeCallbacksRelay(this);
            await DeviceJs.AddResizeCallback(_jsCallbacksRelay.DotNetReference, nameof(_jsCallbacksRelay.NotifyResize));
            await UpdateInlineStyle();
        }
    }

    private async Task UpdateInlineStyle()
    {
        (int? finalV, int? finalH) = (Vertical, Horizontal);

        if (VerticalRelativeToQuery != null)
        {
            (float Width, float Height) bounds = await DomJs.GetElementBounds(VerticalRelativeToQuery);
            finalV = (finalV ?? 0) + (int)bounds.Height;
        }

        if (HorizontalRelativeToQuery != null)
        {
            (float Width, float Height) bounds = await DomJs.GetElementBounds(HorizontalRelativeToQuery);
            finalH = (finalH ?? 0) + (int)bounds.Width;
        }

        InlineStyle = new StringBuilder()
            .Append("display:block;")
            .Append(finalV.HasValue ? $"height:{finalV}px;" : "height:100%;")
            .Append(finalH.HasValue ? $"width:{finalH}px;" : "width:100%;")
            .ToString();

        StateHasChanged();
    }

    public async Task NotifyResize(int windowWidth) => await UpdateInlineStyle();
}
