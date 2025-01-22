using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Abstractions.Components.Features;

public partial class ActivableComponentFeature : NjComponentBase, IComponentFeature<ActivableComponentFeature>
{
    private bool _active;

    /// <summary>
    /// Gets or sets the activity status.
    /// </summary>
    /// <value>
    /// True if active, false if not.
    /// </value>
    /// <remarks>
    /// Setting the value triggers the ActiveChanged event.
    /// </remarks>
    [Parameter]
    public bool Active
    {
        get => _active;
        set
        {
            if (_active == value)
                return;

            _active = value;
            ActiveChanged.InvokeAsync(_active);
        }
    }

    /// <summary>
    /// Gets or sets the event callback for when the active state changes.
    /// </summary>
    /// <value>
    /// The event callback for when the active state changes.
    /// </value>
    [Parameter]
    public EventCallback<bool> ActiveChanged { get; set; }

    /// <summary>
    /// Gets the CSS class for the active state.
    /// </summary>
    /// <value>
    /// The CSS class for the active state if active; otherwise, an empty string.
    /// </value>
    public virtual string ActiveClass => Active ? CssClassReferences.Active : string.Empty;

    ActivableComponentFeature IComponentFeature<ActivableComponentFeature>.Feature => this;

    public void ToggleActive() => Active = !Active;
}