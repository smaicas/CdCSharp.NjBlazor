using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;

// TODO: Refactor this engine to build proper XML using XLinq.
public class XmlEngine : Engine
{
    private const string ElementFormat = "<{0}>{1}</{0}>";

    protected override string PostHighlight(Definition definition, string input) => string.Format("<highlightedInput>{0}</highlightedInput>", input);

    protected override string PreHighlight(Definition definition, string input) => HttpUtility.HtmlEncode(input);

    protected override string ProcessBlockPatternMatch(Definition definition, BlockPattern pattern, Match match) => ProcessPatternMatch(pattern, match);

    protected override string ProcessMarkupPatternMatch(Definition definition, MarkupPattern pattern, Match match)
    {
        StringBuilder builder = new();
        builder.AppendFormat(ElementFormat, "openTag", match.Groups["openTag"].Value);
        builder.AppendFormat(ElementFormat, "whitespace", match.Groups["ws1"].Value);
        builder.AppendFormat(ElementFormat, "tagName", match.Groups["tagName"].Value);

        StringBuilder builder2 = new();
        for (int i = 0; i < match.Groups["attribute"].Captures.Count; i++)
        {
            builder2.AppendFormat(ElementFormat, "whitespace", match.Groups["ws2"].Captures[i].Value);
            builder2.AppendFormat(ElementFormat, "attribName", match.Groups["attribName"].Captures[i].Value);

            if (string.IsNullOrWhiteSpace(match.Groups["attribValue"].Captures[i].Value))
                continue;

            builder2.AppendFormat(ElementFormat, "attribValue", match.Groups["attribValue"].Captures[i].Value);
        }
        builder.AppendFormat(ElementFormat, "attribute", builder2);

        builder.AppendFormat(ElementFormat, "whitespace", match.Groups["ws5"].Value);
        builder.AppendFormat(ElementFormat, "closeTag", match.Groups["closeTag"].Value);

        return string.Format(ElementFormat, pattern.Name, builder);
    }

    protected override string ProcessWordPatternMatch(Definition definition, WordPattern pattern, Match match) => ProcessPatternMatch(pattern, match);

    private string ProcessPatternMatch(Pattern pattern, Match match) => string.Format(ElementFormat, pattern.Name, match.Value);
}