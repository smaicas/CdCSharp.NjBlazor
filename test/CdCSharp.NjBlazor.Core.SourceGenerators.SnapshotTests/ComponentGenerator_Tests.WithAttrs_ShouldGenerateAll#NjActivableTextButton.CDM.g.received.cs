//HintName: NjActivableTextButton.CDM.g.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using CdCSharp.NjBlazor.Core.Components.Variants;

namespace CdCSharp.NjBlazor.Core.Components;
public partial class NjActivableTextButton : NjActivableTextButtonBase
{
    [Parameter]
    public CdCSharp.NjBlazor.Core.Components.NjActivableTextButtonVariant Variant { get; set; } = CdCSharp.NjBlazor.Core.Components.NjActivableTextButtonVariant.UnderLine;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        switch (Variant)
        {
            case CdCSharp.NjBlazor.Core.Components.NjActivableTextButtonVariant.UnderLine:
            {
                builder.OpenComponent<NjActivableTextButtonVariantUnderLine>(0);
                break;
            }

            case CdCSharp.NjBlazor.Core.Components.NjActivableTextButtonVariant.TopLine:
            {
                builder.OpenComponent<NjActivableTextButtonVariantTopLine>(0);
                break;
            }

            case CdCSharp.NjBlazor.Core.Components.NjActivableTextButtonVariant.LeftLine:
            {
                builder.OpenComponent<NjActivableTextButtonVariantLeftLine>(0);
                break;
            }

            case CdCSharp.NjBlazor.Core.Components.NjActivableTextButtonVariant.RightLine:
            {
                builder.OpenComponent<NjActivableTextButtonVariantRightLine>(0);
                break;
            }
        }

        builder.AddAttribute(0, "Active", Active);
        builder.AddAttribute(1, "ActiveChanged", ActiveChanged);
        builder.CloseComponent();
    }

    protected override void OnInitialized()
    {
    }

    protected override void OnParametersSet()
    {
    }

    protected override void OnAfterRender(bool firstRender)
    {
    }

    protected override Task OnInitializedAsync() => Task.CompletedTask;
    protected override Task OnParametersSetAsync() => Task.CompletedTask;
    protected override Task OnAfterRenderAsync(bool firstRender) => Task.CompletedTask;
}