//HintName: NjInputText.CDM.g.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Nj.Blazor.Variants;

namespace Nj.Blazor;
public partial class NjInputText : NjInputTextBase
{
    [Parameter]
    public Nj.Blazor.NjInputTextVariant Variant { get; set; } = Nj.Blazor.NjInputTextVariant.Flat;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        switch (Variant)
        {
            case Nj.Blazor.NjInputTextVariant.Flat:
            {
                builder.OpenComponent<NjInputTextVariantFlat>(0);
                break;
            }

            case Nj.Blazor.NjInputTextVariant.Filled:
            {
                builder.OpenComponent<NjInputTextVariantFilled>(0);
                break;
            }

            case Nj.Blazor.NjInputTextVariant.Outline:
            {
                builder.OpenComponent<NjInputTextVariantOutline>(0);
                break;
            }
        }

        builder.AddAttribute(0, "IsTextArea", IsTextArea);
        builder.AddAttribute(1, "Label", Label);
        builder.AddAttribute(2, "ChildContent", ChildContent);
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