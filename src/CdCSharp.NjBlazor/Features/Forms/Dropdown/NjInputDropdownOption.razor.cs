using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace CdCSharp.NjBlazor.Features.Forms.Dropdown;

/// <summary>
/// Represents an input dropdown option component.
/// </summary>
public partial class NjInputDropdownOption : NjComponentBase
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
    /// Gets or sets the parent dropdown options.
    /// </summary>
    /// <value>
    /// The list of parent dropdown options.
    /// </value>
    [CascadingParameter]
    public List<NjInputDropdownOption>? ParentDropdownOptions { get; set; }

    /// <summary>
    /// Gets or sets the value of the parameter.
    /// </summary>
    /// <value>
    /// The value of the parameter.
    /// </value>
    /// <remarks>
    /// This property is required for editing and does not allow null values.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    [DisallowNull]
    public object Value { get; set; } = default!;

    /// <summary>
    /// This method is called when the parameters are set. It adds the current instance to the
    /// parent dropdown options if it is not already present.
    /// </summary>
    /// <remarks>
    /// If the parent dropdown options are null or if the current instance is already in the parent
    /// dropdown options, no action is taken.
    /// </remarks>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (ParentDropdownOptions == null)
            return;
        if (ParentDropdownOptions.Contains(this))
            return;
        ParentDropdownOptions.Add(this);
    }

    /// <summary>
    /// Determines whether rendering should occur.
    /// </summary>
    /// <returns>
    /// False, indicating that rendering should not occur.
    /// </returns>
    protected override bool ShouldRender() => false;
}