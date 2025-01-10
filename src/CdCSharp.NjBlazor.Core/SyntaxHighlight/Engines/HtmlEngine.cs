using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;

public class HtmlEngine : Engine
{
    private const string ClassSpanFormat = "<span class=\"{0}\">{1}</span>";
    private const string StyleSpanFormat = "<span style=\"{0}\">{1}</span>";
    public bool UseCss { get; set; }

    protected override string PostHighlight(Definition definition, string input)
    {
        ArgumentNullException.ThrowIfNull(definition, nameof(definition));

        if (!UseCss)
        {
            string cssStyle = HtmlEngineHelper.CreatePatternStyle(definition.Style);

            return string.Format(StyleSpanFormat, cssStyle, input);
        }

        string cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, null);

        return string.Format(ClassSpanFormat, cssClassName, input);
    }

    protected override string PreHighlight(Definition definition, string input)
    {
        ArgumentNullException.ThrowIfNull(definition, nameof(definition));

        return HttpUtility.HtmlEncode(input);
    }

    protected override string ProcessBlockPatternMatch(Definition definition, BlockPattern pattern, Match match)
    {
        if (!UseCss)
        {
            string patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.Style);

            return string.Format(StyleSpanFormat, patternStyle, match.Value);
        }

        string cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name);

        return string.Format(ClassSpanFormat, cssClassName, match.Value);
    }

    protected override string ProcessMarkupPatternMatch(Definition definition, MarkupPattern pattern, Match match)
    {
        ArgumentNullException.ThrowIfNull(definition, nameof(definition));
        ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));
        ArgumentNullException.ThrowIfNull(match, nameof(match));

        StringBuilder result = new();
        if (!UseCss)
        {
            string patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.BracketColors, pattern.Style.Font);
            result.AppendFormat(StyleSpanFormat, patternStyle, match.Groups["openTag"].Value);

            result.Append(match.Groups["ws1"].Value);

            patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.Style);
            result.AppendFormat(StyleSpanFormat, patternStyle, match.Groups["tagName"].Value);

            if (pattern.HighlightAttributes)
            {
                string highlightedAttributes = ProcessMarkupPatternAttributeMatches(definition, pattern, match);
                result.Append(highlightedAttributes);
            }

            result.Append(match.Groups["ws5"].Value);

            patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.BracketColors, pattern.Style.Font);
            result.AppendFormat(StyleSpanFormat, patternStyle, match.Groups["closeTag"].Value);
        }
        else
        {
            string cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name + "Bracket");
            result.AppendFormat(ClassSpanFormat, cssClassName, match.Groups["openTag"].Value);

            result.Append(match.Groups["ws1"].Value);

            cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name + "TagName");
            result.AppendFormat(ClassSpanFormat, cssClassName, match.Groups["tagName"].Value);

            if (pattern.HighlightAttributes)
            {
                string highlightedAttributes = ProcessMarkupPatternAttributeMatches(definition, pattern, match);
                result.Append(highlightedAttributes);
            }

            result.Append(match.Groups["ws5"].Value);

            cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name + "Bracket");
            result.AppendFormat(ClassSpanFormat, cssClassName, match.Groups["closeTag"].Value);
        }

        return result.ToString();
    }

    protected override string ProcessWordPatternMatch(Definition definition, WordPattern pattern, Match match)
    {
        if (!UseCss)
        {
            string patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.Style);

            return string.Format(StyleSpanFormat, patternStyle, match.Value);
        }

        string cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name);

        return string.Format(ClassSpanFormat, cssClassName, match.Value);
    }

    private string ProcessMarkupPatternAttributeMatches(Definition definition, MarkupPattern pattern, Match match)
    {
        StringBuilder result = new();

        for (int i = 0; i < match.Groups["attribute"].Captures.Count; i++)
        {
            result.Append(match.Groups["ws2"].Captures[i].Value);
            if (!UseCss)
            {
                string patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.AttributeNameColors, pattern.Style.Font);
                result.AppendFormat(StyleSpanFormat, patternStyle, match.Groups["attribName"].Captures[i].Value);

                if (string.IsNullOrWhiteSpace(match.Groups["attribValue"].Captures[i].Value))
                    continue;

                patternStyle = HtmlEngineHelper.CreatePatternStyle(pattern.AttributeValueColors, pattern.Style.Font);
                result.AppendFormat(StyleSpanFormat, patternStyle, match.Groups["attribValue"].Captures[i].Value);
            }
            else
            {
                string cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name + "AttributeName");
                result.AppendFormat(ClassSpanFormat, cssClassName, match.Groups["attribName"].Captures[i].Value);

                if (string.IsNullOrWhiteSpace(match.Groups["attribValue"].Captures[i].Value))
                    continue;

                cssClassName = HtmlEngineHelper.CreateCssClassName(definition.Name, pattern.Name + "AttributeValue");
                result.AppendFormat(ClassSpanFormat, cssClassName, match.Groups["attribValue"].Captures[i].Value);
            }
        }

        return result.ToString();
    }
}