//HintName: AcCo_AcCoFe.g.cs
using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components.Features;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Core.Components;
public partial class ActivableComponent : NjComponentBase
{
    [Parameter]
    public bool Active { get; set; }

    [Parameter]
    public string ActiveClass { get; set; } = CssClassReferences.Active;
}