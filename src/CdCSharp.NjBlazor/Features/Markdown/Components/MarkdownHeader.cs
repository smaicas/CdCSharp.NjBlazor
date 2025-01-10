using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace CdCSharp.NjBlazor.Features.Markdown.Components;

/// <summary>Represents a Markdown header component.</summary>
internal class MarkdownHeader : ComponentBase
{
    /// <summary>Gets or sets the line value.</summary>
    /// <value>The line value.</value>
    [Parameter]
    [EditorRequired]
    public string Line { get; set; } = default!;

    /// <summary>
    /// Builds the render tree for rendering a header element based on the content of the Line property.
    /// </summary>
    /// <param name="builder">The RenderTreeBuilder used to construct the render tree.</param>
    /// <remarks>
    /// This method overrides the base BuildRenderTree method to render a header element based on the content of the Line property.
    /// It determines the header level based on the number of '#' characters at the beginning of the Line.
    /// </remarks>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        int sequence = 0;
        int headerLevel = 1;
        while (Line != null && Line.Length > headerLevel && Line[headerLevel] == '#')
            headerLevel++;
        builder.OpenElement(++sequence, "h" + (headerLevel + 1));
        builder.AddContent(++sequence, Line[headerLevel..].Trim());
        builder.CloseElement();
    }
}