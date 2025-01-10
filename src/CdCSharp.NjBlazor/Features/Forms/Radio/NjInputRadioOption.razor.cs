using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Radio;

/// <summary>
/// Represents a radio option input component.
/// </summary>
public partial class NjInputRadioOption : NjComponentBase
{
    /// <summary>Gets or sets the list of parent radio button options.</summary>
    /// <value>The list of parent radio button options.</value>
    [CascadingParameter]
    public List<NjInputRadioOption>? ParentRadioButtonOptions { get; set; }

    /// <summary>Gets or sets the value of the parameter.</summary>
    /// <value>The value of the parameter.</value>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// This method is called when the parameters are set. It adds the current instance to the list of ParentRadioButtonOptions if it is not already present.
    /// </summary>
    /// <remarks>
    /// If the ParentRadioButtonOptions list is null or if the current instance is already in the list, no action is taken.
    /// </remarks>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (ParentRadioButtonOptions == null)
            return;
        if (ParentRadioButtonOptions.Contains(this))
            return;
        ParentRadioButtonOptions.Add(this);
    }

    /// <summary>Determines whether rendering should occur.</summary>
    /// <returns>False, indicating that rendering should not occur.</returns>
    protected override bool ShouldRender() => false;
}
