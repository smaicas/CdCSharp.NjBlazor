using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Containers.Components;

/// <summary>
/// Represents an input control component in the Nj framework.
/// </summary>
public partial class NjInputControl : NjComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the component is disabled.
    /// </summary>
    /// <value>
    /// True if the component is disabled; otherwise, false.
    /// </value>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control is a form control.
    /// </summary>
    /// <value>
    /// True if the control is a form control; otherwise, false.
    /// </value>
    [Parameter]
    public bool FormControl { get; set; }

    /// <summary>
    /// Gets the CSS class for the disabled state.
    /// </summary>
    /// <value>
    /// The CSS class for the disabled state. Returns "Disabled" if the component is disabled;
    /// otherwise, returns an empty string.
    /// </value>
    protected string DisabledClass => Disabled ? CssClassReferences.Disabled : string.Empty;

    /// <summary>
    /// Gets the CSS class for the form control.
    /// </summary>
    /// <value>
    /// The CSS class for the form control.
    /// </value>
    protected string FormControlClass =>
        FormControl ? CssClassReferences.FormControl : string.Empty;
}