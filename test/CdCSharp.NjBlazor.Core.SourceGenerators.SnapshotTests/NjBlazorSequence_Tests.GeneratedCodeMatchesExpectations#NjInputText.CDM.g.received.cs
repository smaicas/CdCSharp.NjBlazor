//HintName: NjInputText.CDM.g.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using CdCSharp.NjBlazor.Features.Forms.Text.Variants;

namespace CdCSharp.NjBlazor.Features.Forms.Text;
public partial class NjInputText : NjInputTextBase
{
    [Parameter]
    public CdCSharp.NjBlazor.Features.Forms.Text.NjInputTextVariant Variant { get; set; } = CdCSharp.NjBlazor.Features.Forms.Text.NjInputTextVariant.Flat;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        switch (Variant)
        {
            case CdCSharp.NjBlazor.Features.Forms.Text.NjInputTextVariant.Flat:
            {
                builder.OpenComponent<NjInputTextVariantFlat>(0);
                break;
            }

            case CdCSharp.NjBlazor.Features.Forms.Text.NjInputTextVariant.Filled:
            {
                builder.OpenComponent<NjInputTextVariantFilled>(0);
                break;
            }

            case CdCSharp.NjBlazor.Features.Forms.Text.NjInputTextVariant.Outline:
            {
                builder.OpenComponent<NjInputTextVariantOutline>(0);
                break;
            }
        }

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