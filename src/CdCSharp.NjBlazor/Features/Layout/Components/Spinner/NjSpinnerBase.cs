using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Spinner;
public class NjSpinnerBase : NjComponentBase
{
    [Parameter]
    public CssColor? Color { get; set; }
}
