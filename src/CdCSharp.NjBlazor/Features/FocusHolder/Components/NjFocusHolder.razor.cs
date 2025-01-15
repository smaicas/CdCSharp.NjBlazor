using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CdCSharp.NjBlazor.Features.FocusHolder.Components;

/// <summary>
/// Represents a partial class for the NjFocusHolder that extends the NjComponentBase class.
/// </summary>
public partial class NjFocusHolder : NjComponentBase
{
    private ElementReference? _focusHolderReference;

    private bool focused;

    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the query selector for the focus holder element.
    /// </summary>
    /// <value>
    /// The query selector for the focus holder element.
    /// </value>
    [Parameter]
    [EditorRequired]
    public string FocusHolderQuerySelector { get; set; } = default!;

    /// <summary>
    /// Gets or sets the event callback for mouse click events.
    /// </summary>
    /// <value>
    /// The event callback for mouse click events.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets the event callback for focus events.
    /// </summary>
    /// <value>
    /// The event callback for focus events.
    /// </value>
    [Parameter]
    public EventCallback<FocusEventArgs> OnFocus { get; set; }

    /// <summary>
    /// Gets or sets the event callback for the focus out event.
    /// </summary>
    /// <value>
    /// The event callback for the focus out event.
    /// </value>
    [Parameter]
    public EventCallback<FocusEventArgs> OnFocusOut { get; set; }

    /// <summary>
    /// Gets or sets the JavaScript interop service for handling focus holder.
    /// </summary>
    /// <value>
    /// The JavaScript interop service for handling focus holder.
    /// </value>
    [Inject]
    protected IDOMJsInterop DOMJs { get; set; } = default!;

    private async Task DoFocusAsync(FocusEventArgs e)
    {
        if (focused)
        {
            return;
        }
        if (FocusHolderQuerySelector == null)
        {
            throw new ArgumentNullException(nameof(FocusHolderQuerySelector));
        }
        focused = true;
        await OnFocus.InvokeAsync(e);
        await DOMJs.FocusElementAsync(FocusHolderQuerySelector, _focusHolderReference);
    }

    private async Task DoFocusOutAsync(FocusEventArgs e)
    {
        focused = false;
        await OnFocusOut.InvokeAsync();
    }
}