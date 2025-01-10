using CdCSharp.NjBlazor.Features.Media.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Forms.Dropdown.IconDropdown;
public partial class NjIconDropdown<TValue> : NjInputDropdownBase<TValue>
{
    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public NjSvgIconSize IconSize { get; set; }
}