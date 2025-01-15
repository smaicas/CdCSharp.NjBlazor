using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Abstractions.Components;

/// <summary>
/// Base class for control components in the Nj framework.
/// </summary>
public abstract class NjControlComponentBase : NjComponentBase
{
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
}