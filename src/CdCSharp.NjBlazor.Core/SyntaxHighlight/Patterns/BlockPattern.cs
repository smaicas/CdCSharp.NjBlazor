using System.Text.RegularExpressions;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents a block pattern.
/// </summary>
public sealed class BlockPattern : Pattern
{
    /// <summary>
    /// Initializes a new instance of the BlockPattern class with the specified parameters.
    /// </summary>
    /// <param name="name">
    /// The name of the block pattern.
    /// </param>
    /// <param name="style">
    /// The style of the block pattern.
    /// </param>
    /// <param name="beginsWith">
    /// The string that marks the beginning of the block.
    /// </param>
    /// <param name="endsWith">
    /// The string that marks the end of the block.
    /// </param>
    /// <param name="escapesWith">
    /// The string used for escaping special characters within the block.
    /// </param>
    public BlockPattern(string name, Style style, string beginsWith, string endsWith, string? escapesWith)
        : base(name, style)
    {
        BeginsWith = beginsWith;
        EndsWith = endsWith;
        EscapesWith = escapesWith;
    }

    /// <summary>
    /// Gets the value indicating the string that the property begins with.
    /// </summary>
    /// <value>
    /// The string that the property begins with.
    /// </value>
    public string BeginsWith { get; private set; }

    /// <summary>
    /// Gets the suffix that the string ends with.
    /// </summary>
    /// <value>
    /// The suffix that the string ends with.
    /// </value>
    public string EndsWith { get; private set; }

    /// <summary>
    /// Gets or sets the escape character used for escaping special characters.
    /// </summary>
    /// <value>
    /// The escape character used for escaping special characters.
    /// </value>
    public string? EscapesWith { get; private set; }

    /// <summary>
    /// Escapes special characters in a given string.
    /// </summary>
    /// <param name="str">
    /// The input string to escape special characters.
    /// </param>
    /// <returns>
    /// The input string with special characters escaped.
    /// </returns>
    public static string Escape(string str)
    {
        if (str.CompareTo(@"\n") != 0)
            str = Regex.Escape(str);

        return str;
    }

    /// <summary>
    /// Gets the regular expression pattern based on the specified BeginsWith, EndsWith, and
    /// EscapesWith strings.
    /// </summary>
    /// <returns>
    /// The regular expression pattern.
    /// </returns>
    public override string GetRegexPattern()
    {
        if (string.IsNullOrEmpty(EscapesWith))
        {
            if (EndsWith.CompareTo(@"\n") == 0)
                return string.Format(@"{0}[^\n\r]*", Escape(BeginsWith));

            return string.Format(@"{0}[\w\W\s\S]*?{1}", Escape(BeginsWith), Escape(EndsWith));
        }

        return string.Format("{0}(?>{1}.|[^{2}]|.)*?{3}", new object[] { Regex.Escape(BeginsWith), Regex.Escape(EscapesWith.Substring(0, 1)), Regex.Escape(EndsWith.Substring(0, 1)), Regex.Escape(EndsWith) });
    }
}