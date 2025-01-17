//HintName: NjInputText.ComponentDeMux.g.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Nj.Blazor.Variants;

namespace Nj.Blazor;
public partial class NjInputText
{
    [Parameter]
    public NjInputTextVariant Variant { get; set; } = NjInputTextVariant.Flat;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        switch (Variant)
        {
            case NjInputTextVariant.Flat:
            {
                builder.OpenComponent<NjInputTextVariantFlat>(0);
                break;
            }

            case NjInputTextVariant.Filled:
            {
                builder.OpenComponent<NjInputTextVariantFilled>(0);
                break;
            }

            case NjInputTextVariant.Outline:
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