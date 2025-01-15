using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Draggable.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Features.Draggable.Components;

/// <summary>
/// Base class for draggable components in the Nj framework.
/// </summary>
/// <remarks>
/// This class serves as a base for components that can be dragged within the Nj framework. It
/// extends the NjComponentBase class and implements the INjDraggableJsCallbacks interface.
/// </remarks>
public abstract class NjDraggableBase : NjComponentBase, INjDraggableJsCallbacks
{
    /// <summary>
    /// Field to hold a reference to a JavaScript callbacks relay object.
    /// </summary>
    /// <value>
    /// The JavaScript callbacks relay object.
    /// </value>
    [DisallowNull]
    internal NjDraggableJsCallbackRelay? _jsCallbacksRelay;

    /// <summary>
    /// Reference to an element in the DOM.
    /// </summary>
    protected ElementReference elementRef;

    private bool _isMouseDown;

    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the event callback for mouse drag events.
    /// </summary>
    /// <value>
    /// The event callback for mouse drag events.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnDrag { get; set; }

    /// <summary>
    /// Gets or sets the event callback for when a drag operation ends.
    /// </summary>
    /// <value>
    /// The event callback for when a drag operation ends.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnDragEnds { get; set; }

    /// <summary>
    /// Gets or sets the event callback for handling the drag start event.
    /// </summary>
    /// <value>
    /// The event callback for handling the drag start event.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnDragStart { get; set; }

    /// <summary>
    /// Gets or sets the callback for the mouse leave event.
    /// </summary>
    /// <value>
    /// The callback for the mouse leave event.
    /// </value>
    [Parameter]
    public EventCallback<MouseEventArgs> OnMouseLeave { get; set; }

    /// <summary>
    /// Gets or sets the DraggableJsInterop service for enabling drag-and-drop functionality.
    /// </summary>
    /// <value>
    /// The DraggableJsInterop service.
    /// </value>
    [Inject]
    protected IDraggableJsInterop DraggableJs { get; set; } = default!;

    /// <summary>
    /// Notifies about a mouse move asynchronously.
    /// </summary>
    /// <param name="mouseEventArgs">
    /// The mouse event arguments.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public async Task NotifyMouseMoveAsync(MouseEventArgs mouseEventArgs) =>
        await OnDrag.InvokeAsync(mouseEventArgs);

    /// <summary>
    /// Initiates a drag operation in response to a mouse event.
    /// </summary>
    /// <param name="args">
    /// The MouseEventArgs containing information about the mouse event.
    /// </param>
    /// <returns>
    /// An asynchronous Task.
    /// </returns>
    protected async Task BeginDrag(MouseEventArgs args)
    {
        _isMouseDown = true;
        await EnableMouseMove();
        await OnDragStart.InvokeAsync(args);
    }

    /// <summary>
    /// Handles the mouse leaving event.
    /// </summary>
    /// <param name="args">
    /// The MouseEventArgs containing information about the mouse event.
    /// </param>
    /// <returns>
    /// A Task representing the asynchronous operation.
    /// </returns>
    protected async Task Leave(MouseEventArgs args)
    {
        _isMouseDown = false;
        await DisableMouseMove();
        await OnMouseLeave.InvokeAsync(args);
    }

    /// <summary>
    /// Method called after the component has been rendered.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if this is the first render of the component.
    /// </param>
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _jsCallbacksRelay = new NjDraggableJsCallbackRelay(this);
        }
    }

    /// <summary>
    /// Stops the drag operation and triggers the event for when the drag ends.
    /// </summary>
    /// <param name="args">
    /// The MouseEventArgs containing information about the mouse event.
    /// </param>
    /// <returns>
    /// An asynchronous Task.
    /// </returns>
    protected async Task StopDrag(MouseEventArgs args)
    {
        _isMouseDown = false;
        await DisableMouseMove();
        await OnDragEnds.InvokeAsync(args);
    }

    private async Task DisableMouseMove() => await DraggableJs.DisableMouseMoveAsync(elementRef);

    private async Task EnableMouseMove() =>
        await DraggableJs.EnableMouseMoveAsync(
            elementRef,
            _jsCallbacksRelay?.DotNetReference
                ?? throw new ArgumentNullException("Required _jsCallbacksRelay not set")
        );
}