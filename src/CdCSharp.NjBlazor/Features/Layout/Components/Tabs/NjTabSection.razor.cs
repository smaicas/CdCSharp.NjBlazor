using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Tabs;

/// <summary>
/// Represents a tab section component.
/// </summary>
/// <remarks>
/// This class extends the functionality of the base component <see cref="NjComponentBase"/>.
/// </remarks>
public partial class NjTabSection : NjComponentBase
{
    /// <summary>Gets or sets the parent NjTabs component for cascading parameters.</summary>
    [CascadingParameter]
    public NjTabs Parent { get; set; }

    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <remarks>This property allows for rendering child components within the parent component.</remarks>
    [Parameter]
    public RenderFragment ChildContent { get; set; }

    /// <summary>Gets or sets the header text.</summary>
    /// <value>The header text.</value>
    [Parameter]
    [EditorRequired]
    public string? Header { get; set; }

    /// <summary>Represents the active state.</summary>
    public bool Active;

    /// <summary>
    /// This method is called when the parameters are set.
    /// It adds the current section to its parent.
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Parent.AddSection(this);
    }

    /// <summary>Determines whether rendering should occur.</summary>
    /// <returns>False, indicating that rendering should not occur.</returns>
    protected override bool ShouldRender() => false;
}
