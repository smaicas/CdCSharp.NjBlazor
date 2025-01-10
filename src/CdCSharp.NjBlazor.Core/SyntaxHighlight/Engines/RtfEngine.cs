using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;

// TODO: Clean up and refactor big methods into smaller, more manageable chunks.
public class RtfEngine : Engine
{
    private const string RtfFormat = "{0} {1}";
    private readonly ArrayList colors = [];
    private readonly ArrayList fonts = [];

    protected override string PostHighlight(Definition definition, string input)
    {
        string result = input
            .Replace("{", @"\{").Replace("}", @"\}").Replace("\t", @"\tab ")
            .Replace("\r\n", @"\par ");
        string fontList = BuildFontList();
        string colorList = BuildColorList();

        return string.Format(@"{{\rtf1\ansi{{\fonttbl{{{0}}}}}{{\colortbl;{1}}}{2}}}", fontList, colorList, result);
    }

    protected override string PreHighlight(Definition definition, string input) => HttpUtility.HtmlEncode(input);

    protected override string ProcessBlockPatternMatch(Definition definition, BlockPattern pattern, Match match)
    {
        string style = CreateRtfPatternStyle(pattern.Style.Colors.ForeColor, pattern.Style.Colors.BackColor, pattern.Style.Font);

        return "{" + string.Format(RtfFormat, style, match.Value) + "}";
    }

    protected override string ProcessMarkupPatternMatch(Definition definition, MarkupPattern pattern, Match match)
    {
        StringBuilder builder = new();
        string style = CreateRtfPatternStyle(pattern.Style.Colors.ForeColor, pattern.Style.Colors.BackColor, pattern.Style.Font);
        string bracketStyle = CreateRtfPatternStyle(pattern.BracketColors.ForeColor, pattern.BracketColors.BackColor, pattern.Style.Font);
        string attributeNameStyle = null;
        string attributeValueStyle = null;
        if (pattern.HighlightAttributes)
        {
            attributeNameStyle = CreateRtfPatternStyle(pattern.AttributeNameColors.ForeColor, pattern.AttributeNameColors.BackColor, pattern.Style.Font);
            attributeValueStyle = CreateRtfPatternStyle(pattern.AttributeValueColors.ForeColor, pattern.AttributeValueColors.BackColor, pattern.Style.Font);
        }
        builder.AppendFormat(RtfFormat, bracketStyle, match.Groups["openTag"].Value);
        builder.Append(match.Groups["ws1"].Value);
        builder.AppendFormat(RtfFormat, style, match.Groups["tagName"].Value);
        if (attributeNameStyle != null)
            for (int i = 0; i < match.Groups["attribute"].Captures.Count; i++)
            {
                builder.Append(match.Groups["ws2"].Captures[i].Value);
                builder.AppendFormat(RtfFormat, attributeNameStyle, match.Groups["attribName"].Captures[i].Value);

                if (string.IsNullOrWhiteSpace(match.Groups["attribValue"].Captures[i].Value))
                    continue;

                builder.AppendFormat(RtfFormat, attributeValueStyle, match.Groups["attribValue"].Captures[i].Value);
            }
        builder.Append(match.Groups["ws5"].Value);
        builder.AppendFormat(RtfFormat, bracketStyle, match.Groups["closeTag"].Value);

        return "{" + builder + "}";
    }

    protected override string ProcessWordPatternMatch(Definition definition, WordPattern pattern, Match match)
    {
        string style = CreateRtfPatternStyle(pattern.Style.Colors.ForeColor, pattern.Style.Colors.BackColor, pattern.Style.Font);

        return "{" + string.Format(RtfFormat, style, match.Value) + "}";
    }

    private string BuildColorList()
    {
        StringBuilder builder = new();
        foreach (HexColor hexColor in colors.Cast<HexColor>())
            builder.AppendFormat(@"\red{0}\green{1}\blue{2};", hexColor.Red, hexColor.Green, hexColor.Blue);
        return builder.ToString();
    }

    private string BuildFontList()
    {
        StringBuilder builder = new();
        for (int i = 0; i < fonts.Count; i++)
            builder.AppendFormat(@"\f{0} {1};", i, fonts[i]);
        return builder.ToString();
    }

    private string CreateRtfPatternStyle(Color foreColor, Color backColor, Font font) => string.Concat(new object[] { @"\cf", GetIndexOfColor(foreColor), @"\highlight", GetIndexOfColor(backColor), @"\f", GetIndexOfFont(font.Name), @"\fs", font.Size * 2f });

    private int GetIndexOfColor(Color color)
    {
        HexColor color2 = new();
        if (color.Name.IndexOf("#") > -1)
        {
            color2.Red = int.Parse(color.Name.Substring(1, 2), NumberStyles.AllowHexSpecifier);
            color2.Green = int.Parse(color.Name.Substring(3, 2), NumberStyles.AllowHexSpecifier);
            color2.Blue = int.Parse(color.Name.Substring(5, 2), NumberStyles.AllowHexSpecifier);
        }
        else
        {
            color2.Red = color.R;
            color2.Green = color.G;
            color2.Blue = color.B;
        }
        int index = colors.IndexOf(color2);
        if (index > -1)
            return index + 1;
        colors.Add(color2);

        return colors.Count;
    }

    private int GetIndexOfFont(string font)
    {
        int index = fonts.IndexOf(font);
        if (index > -1)
            return index + 1;
        fonts.Add(font);

        return fonts.Count;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct HexColor
    {
        public int Red;
        public int Green;
        public int Blue;
    }
}