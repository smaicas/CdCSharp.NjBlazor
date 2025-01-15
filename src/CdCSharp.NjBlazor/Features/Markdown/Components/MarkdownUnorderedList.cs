using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace CdCSharp.NjBlazor.Features.Markdown.Components;

/// <summary>
/// Represents a component for rendering an unordered list in Markdown format.
/// </summary>
internal class MarkdownUnorderedList : ComponentBase
{
    /// <summary>
    /// Gets or sets a list of strings representing lines of text.
    /// </summary>
    /// <value>
    /// The list of strings representing lines of text.
    /// </value>
    [Parameter]
    public List<string> Lines { get; set; } = [];

    /// <summary>
    /// Builds the render tree for the component.
    /// </summary>
    /// <param name="builder">
    /// The RenderTreeBuilder used to build the render tree.
    /// </param>
    /// <remarks>
    /// This method overrides the base class method to build the render tree for the component. It
    /// iterates over the Lines collection to render each line in the component.
    /// </remarks>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        int sequence = 0;
        int currentIndex = 0;
        RenderList(builder, Lines, ref sequence, 0, ref currentIndex);
    }

    private int GetIndentationLevel(string line)
    {
        int level = 0;
        foreach (char c in line)
            if (c == '\t')
                level++;
            else
                break;
        return level;
    }

    private void RenderList(RenderTreeBuilder builder, List<string> lines, ref int sequence, int level, ref int currentIndex)
    {
        if (lines == null || lines.Count == 0)
            return;

        builder.OpenElement(++sequence, "ul");

        while (currentIndex < lines.Count)
        {
            string line = lines[currentIndex];
            if (string.IsNullOrWhiteSpace(line))
            {
                currentIndex++;
                continue;
            }

            int currentLevel = GetIndentationLevel(line);

            if (currentLevel == level)
            {
                builder.OpenElement(++sequence, "li");
                builder.AddMarkupContent(++sequence, line.TrimStart('\t').Substring(2));
                builder.CloseElement();
                currentIndex++;
            }
            else if (currentLevel > level)
                RenderList(builder, lines, ref sequence, currentLevel, ref currentIndex);
            else
                break;
        }

        builder.CloseElement();
    }
}