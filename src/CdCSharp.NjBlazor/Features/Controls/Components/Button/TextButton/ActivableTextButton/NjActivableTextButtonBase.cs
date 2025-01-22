using CdCSharp.NjBlazor.Core.Abstractions.Components.Features;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CdCSharp.NjBlazor.Features.Controls.Components.Button.TextButton.ActivableTextButton;

/// <summary>
/// Base class for an activable text button.
/// </summary>
//[ComponentFeatures<NjComponentBase>(typeof(ActivableComponentFeature))]
public partial class NjActivableTextButtonBase : NjTextButtonBase
{
    [Inject]
    public IComponentFeature<ActivableComponentFeature> ActivableFeature { get; set; } = default!;
    protected virtual async Task ProcessClickAsync(MouseEventArgs? mouseEventArgs)
    {
        await OnClick.InvokeAsync(mouseEventArgs);
        ActivableFeature.Feature.ToggleActive();
    }
}
