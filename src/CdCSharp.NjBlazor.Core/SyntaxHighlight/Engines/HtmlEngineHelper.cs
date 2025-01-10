using CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;
using System.Drawing;
using System.Text;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Engines;

internal static class HtmlEngineHelper
{
    public static string CreateCssClassName(string definition, string pattern)
    {
        string cssClassName = definition
            .Replace("#", "sharp")
            .Replace("+", "plus")
            .Replace(".", "dot")
            .Replace("-", "");

        return string.Concat(cssClassName, pattern);
    }

    public static string CreatePatternStyle(Style style) => CreatePatternStyle(style.Colors, style.Font);

    public static string CreatePatternStyle(ColorPair colors, Font font)
    {
        StringBuilder patternStyle = new();
        if (colors != null)
        {
            if (colors.ForeColor != Color.Empty)
                patternStyle.Append("color: " + colors.ForeColor.Name + ";");
            if (colors.BackColor != Color.Empty)
                patternStyle.Append("background-color: " + colors.BackColor.Name + ";");
        }

        if (font != null)
        {
            if (font.Name != null)
                patternStyle.Append("font-family: " + font.Name + ";");
            if (font.Size > 0f)
                patternStyle.Append("font-size: " + font.Size + "px;");
            if (font.Style is not FontStyle.Bold and not FontStyle.BoldItalic)
                patternStyle.Append("font-weight: normal;");
            if (font.Style is FontStyle.Bold or FontStyle.BoldItalic)
                patternStyle.Append("font-weight: bold;");
        }

        return patternStyle.ToString();
    }
}