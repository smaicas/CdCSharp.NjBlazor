using CdCSharp.NjBlazor.Core.Collections;
using Microsoft.AspNetCore.Components;
using Nj.Blazor.Markdown;

namespace CdCSharp.NjBlazor.Features.Markdown;

/// <summary>
/// Parses Markdown content into a RenderFragment for rendering.
/// </summary>
public class MarkdownToRenderFragmentParser
{
    /// <summary>
    /// Parses the given text and generates a RenderFragment representing the formatted content.
    /// </summary>
    /// <param name="text">The text to parse.</param>
    /// <returns>A RenderFragment representing the formatted content.</returns>
    public static RenderFragment ParseText(string text)
    {
        List<RenderFragment> fragments = [];

        string[] lines = text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
        lines = lines.RemoveUnpairedEmptyStrings();

        for (int index = 0; index < lines.Length; index++)
        {
            string line = lines[index];

            if (line.IsHeader())
                fragments.Add(line.RenderHeader());
            else if (line.IsUnorderedList())
            {
                List<string> listLines = [lines[index]];
                while (index < lines.Length - 1 && lines[index + 1].IsUnorderedList())
                {
                    index = index + 1;
                    listLines.Add(lines[index]);
                }
                listLines = listLines.Select(RenderHtmlStringExtensions.ProcessInlineItems).ToList();
                fragments.Add(listLines.RenderUnorderedList());
            }
            else if (line.IsBlockQuote())
                fragments.Add(line.RenderBlockQuote());
            else if (line.IsLineSeparator())
                fragments.Add(builder =>
                {
                    builder.OpenElement(0, "hr");
                    builder.CloseElement();
                });
            else if (line.IsCodeBlock())
            {
                List<string> listLines = [];

                string language = line.Length > 3 ? line.Substring(3) : string.Empty;
                while (index < lines.Length - 1 && !lines[index + 1].IsCodeBlock())
                {
                    index = index + 1;
                    listLines.Add(lines[index]);
                }
                fragments.Add(listLines.RenderCodeBlock(language));
                index++;
            }
            else
            {
                List<string> listLines = [lines[index]];
                while (index < lines.Length - 1 && lines[index + 1].IsParagraph())
                {
                    index = index + 1;
                    listLines.Add(lines[index]);
                }

                fragments.Add(listLines.RenderParagraph());
            }
        }
        return builder =>
        {
            foreach ((RenderFragment value, int index) in fragments.WithIndex())
                builder.AddContent(index, value);
        };
    }
}

/// <summary>
/// Contains extension methods for removing empty strings from collections.
/// </summary>
public static class EmptyStringRemoverExtensions
{
    /// <summary>Removes unpaired empty strings from the input array.</summary>
    /// <param name="array">The input string array.</param>
    /// <returns>A new string array with unpaired empty strings removed.</returns>
    public static string[] RemoveUnpairedEmptyStrings(this string[] array)
    {
        List<string> result = [];
        bool skipNext = false;

        for (int i = 0; i < array.Length; i++)
        {
            if (skipNext)
            {
                skipNext = false;
                continue;
            }

            if (string.IsNullOrWhiteSpace(array[i]))
            {
                if (i < array.Length - 1 && string.IsNullOrWhiteSpace(array[i + 1]))
                {
                    result.Add(array[i]);
                    skipNext = true;
                }
            }
            else
            {
                result.Add(array[i]);
            }
        }

        return result.ToArray();
    }
}