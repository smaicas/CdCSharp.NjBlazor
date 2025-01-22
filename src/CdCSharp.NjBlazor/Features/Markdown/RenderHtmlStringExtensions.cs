using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.SyntaxHighlight;
using CdCSharp.NjBlazor.Features.Markdown.Components;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Features.Markdown;

/// <summary>
/// Contains extension methods for rendering HTML strings.
/// </summary>
internal static partial class RenderHtmlStringExtensions
{
    /// <summary>
    /// Processes inline items in a given line of text.
    /// </summary>
    /// <param name="line">
    /// The line of text to process.
    /// </param>
    /// <returns>
    /// The processed line of text with inline items formatted as HTML elements.
    /// </returns>
    public static string ProcessInlineItems(string line)
    {
        // Bold
        line = Bold().Replace(line, match => $"<b>{match.Groups[1].Value.Trim()}</b>");
        // Italic
        line = Italic().Replace(line, match => $"<i>{match.Groups[1].Value.Trim()}</i>");
        // Image
        line = Image().Replace(line, match => $"<img src=\"{match.Groups[2].Value.Trim()}\" alt=\"{match.Groups[1].Value.Trim()}\">");
        // Link
        line = Link().Replace(line, match => $"<a class=\"{CssClassReferences.Text.Underline} {CssClassReferences.Pointer}\" href=\"{match.Groups[2].Value.Trim()}\">{match.Groups[1].Value.Trim()}</a>");
        // Inline code
        line = InlineCode().Replace(line, match => $"<code>{match.Groups[1].Value.Trim()}</code>");

        return line;
    }

    /// <summary>
    /// Renders a blockquote element with the provided line as content.
    /// </summary>
    /// <param name="line">
    /// The line to be rendered within the blockquote element.
    /// </param>
    /// <returns>
    /// A RenderFragment representing the blockquote element with the specified content.
    /// </returns>
    internal static RenderFragment RenderBlockQuote(this string line)
    {
        return builder =>
        {
            builder.OpenElement(0, "blockquote");
            builder.AddContent(1, line.Substring(2));
            builder.CloseElement();
            builder.OpenElement(2, "br");
            builder.CloseElement();
        };
    }

    /// <summary>
    /// Renders a code block with syntax highlighting based on the specified language.
    /// </summary>
    /// <param name="lines">
    /// The lines of code to be rendered.
    /// </param>
    /// <param name="language">
    /// The language of the code for syntax highlighting.
    /// </param>
    /// <returns>
    /// A RenderFragment representing the code block with syntax highlighting.
    /// </returns>
    internal static RenderFragment RenderCodeBlock(this IEnumerable<string> lines, string language)
    {
        string code = string.Join(Environment.NewLine, lines);

        SyntaxHighlightLanguage codeLanguage = language.ToLower() switch
        {
            "aspx" => SyntaxHighlightLanguage.Aspx,
            "c" => SyntaxHighlightLanguage.C,
            "cobol" => SyntaxHighlightLanguage.Cobol,
            "c++" or "cplusplus" => SyntaxHighlightLanguage.CPlusPlus,
            "csharp" or "c#" => SyntaxHighlightLanguage.CSharp,
            "eiffel" => SyntaxHighlightLanguage.Eiffel,
            "fortran" => SyntaxHighlightLanguage.Fortran,
            "haskell" => SyntaxHighlightLanguage.Haskell,
            "html" => SyntaxHighlightLanguage.Html,
            "java" => SyntaxHighlightLanguage.Java,
            "javascript" or "js" => SyntaxHighlightLanguage.JavaScript,
            "mercury" => SyntaxHighlightLanguage.Mercury,
            "msil" => SyntaxHighlightLanguage.Msil,
            "pascal" => SyntaxHighlightLanguage.Pascal,
            "perl" => SyntaxHighlightLanguage.Perl,
            "php" => SyntaxHighlightLanguage.Php,
            "python" or "py" => SyntaxHighlightLanguage.Python,
            "ruby" => SyntaxHighlightLanguage.Ruby,
            "sql" => SyntaxHighlightLanguage.Sql,
            "vbnet" or "visualbasicnet" => SyntaxHighlightLanguage.VBNET,
            "vbscript" or "visualbasicscript" => SyntaxHighlightLanguage.VBScript,
            "vb" or "visualbasic" => SyntaxHighlightLanguage.VisualBasic,
            "xml" => SyntaxHighlightLanguage.Xml,
            _ => SyntaxHighlightLanguage.None
        };

        Dictionary<string, object> attr = new()
        {
            { "Code", code },
            { "Language", codeLanguage },
        };

        return builder =>
        {
            builder.OpenComponent<NjCodeHighlight>(0);
            builder.AddMultipleAttributes(1, attr);
            builder.CloseComponent();
            builder.OpenElement(2, "br");
            builder.CloseElement();
        };
    }

    /// <summary>
    /// Renders a header component for a given line of text.
    /// </summary>
    /// <param name="line">
    /// The text content of the header.
    /// </param>
    /// <returns>
    /// A RenderFragment representing the header component.
    /// </returns>
    internal static RenderFragment RenderHeader(this string line)
    {
        Dictionary<string, object> attr = new()
        {
            { "Line", line },
        };

        return builder =>
        {
            builder.OpenComponent<MarkdownHeader>(0);
            builder.AddMultipleAttributes(1, attr);
            builder.CloseComponent();
        };
    }

    /// <summary>
    /// Renders a collection of strings as a paragraph in a Blazor component.
    /// </summary>
    /// <param name="lines">
    /// The collection of strings to render as lines in the paragraph.
    /// </param>
    /// <returns>
    /// A RenderFragment representing the paragraph with the provided lines.
    /// </returns>
    internal static RenderFragment RenderParagraph(this IEnumerable<string> lines)
    {
        return builder =>
        {
            builder.OpenElement(0, "p");
            int sequence = 1;
            foreach (string line in lines)
            {
                string lineReplaced = ProcessInlineItems(line);
                builder.AddMarkupContent(++sequence, lineReplaced);
                builder.OpenElement(++sequence, "br");
                builder.CloseElement();
            }
            builder.CloseElement();
        };
    }

    /// <summary>
    /// Renders an unordered list based on the provided lines.
    /// </summary>
    /// <param name="lines">
    /// The lines to be rendered as list items.
    /// </param>
    /// <returns>
    /// A RenderFragment representing the unordered list.
    /// </returns>
    internal static RenderFragment RenderUnorderedList(this IEnumerable<string> lines)
    {
        Dictionary<string, object> attr = new()
        {
            { "Lines", lines },
        };

        return builder =>
        {
            builder.OpenComponent<MarkdownUnorderedList>(0);
            builder.AddMultipleAttributes(1, attr);
            builder.CloseComponent();
            builder.OpenElement(2, "br");
            builder.CloseElement();
        };
    }

    [GeneratedRegex(@"\*\*(.*?)\*\*")]
    private static partial Regex Bold();

    [GeneratedRegex(@"\!\[(.*?)\]\((.*?)\)")]
    private static partial Regex Image();

    [GeneratedRegex(@"\`(.*?)\`")]
    private static partial Regex InlineCode();

    [GeneratedRegex(@"\*(.*?)\*")]
    private static partial Regex Italic();

    //[GeneratedRegex(@"(?=\[(.+?)\]\((.+?)\))")]
    [GeneratedRegex(@"\[([^\]\[]*?)\]\(([^\)\(]*?)\)")]
    private static partial Regex Link();
}