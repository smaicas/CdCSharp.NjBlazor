using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Containers.Components;

/// <summary>
/// Represents a partial class for fitting content within a component in Nj framework.
/// </summary>
public partial class NjFitContent : NjComponentBase
{
    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
