using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Sidebar;
public partial class NjSidebar : NjComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private bool IsOpen { get; set; } = true;

    private string OpenClass => IsOpen ? CssClassReferences.Open : string.Empty;
    private void ToggleOpen() => IsOpen = !IsOpen;
}
