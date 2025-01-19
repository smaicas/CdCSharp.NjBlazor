using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Layout.Components.Spinner;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Markdown.Components;

/// <summary>
/// Component to render markdown as RenderFragment
/// </summary>
public partial class NjMarkdownFragment : NjComponentBase
{
    /// <summary>
    /// The string content
    /// </summary>
    [Parameter]
    public Func<Task<string?>>? Content { get; set; }

    [Parameter]
    public NjSpinnerVariant SpinnerVariant { get; set; } = NjSpinnerVariant.Default;

    public RenderFragment? FinalContent { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Content != null && firstRender)
        {
            string? content = await Content();
            if (content != null)
            {
                FinalContent = MarkdownToRenderFragmentParser.ParseText(content);
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
