using System.Text;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents a definition of a concept or term.
/// </summary>
public class Definition
{
    /// <summary>
    /// Initializes a new instance of the Definition class.
    /// </summary>
    /// <param name="name">
    /// The name of the definition.
    /// </param>
    /// <param name="caseSensitive">
    /// A boolean indicating whether the definition is case-sensitive.
    /// </param>
    /// <param name="style">
    /// The style associated with the definition.
    /// </param>
    /// <param name="patterns">
    /// A dictionary containing patterns associated with the definition.
    /// </param>
    public Definition(string name, bool caseSensitive, Style style, IDictionary<string, Pattern> patterns)
    {
        Name = name;
        CaseSensitive = caseSensitive;
        Style = style;
        Patterns = patterns;
    }

    /// <summary>
    /// Gets a value indicating whether the comparison is case-sensitive.
    /// </summary>
    /// <value>
    /// True if the comparison is case-sensitive; otherwise, false.
    /// </value>
    public bool CaseSensitive { get; private set; }

    /// <summary>
    /// Gets the name property.
    /// </summary>
    /// <value>
    /// The name of the object.
    /// </value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the dictionary of patterns.
    /// </summary>
    /// <value>
    /// The dictionary containing string keys and Pattern values.
    /// </value>
    public IDictionary<string, Pattern> Patterns { get; private set; }

    /// <summary>
    /// Gets or sets the style of the element.
    /// </summary>
    /// <value>
    /// The style of the element.
    /// </value>
    public Style Style { get; private set; }

    /// <summary>
    /// Generates a regex pattern based on different types of patterns (Block, Markup, Word).
    /// </summary>
    /// <returns>
    /// A string representing the combined regex pattern.
    /// </returns>
    public string GetRegexPattern()
    {
        StringBuilder allPatterns = new();
        StringBuilder blockPatterns = new();
        StringBuilder markupPatterns = new();
        StringBuilder wordPatterns = new();

        foreach (Pattern pattern in Patterns.Values)
            if (pattern is BlockPattern)
            {
                if (blockPatterns.Length > 1)
                    blockPatterns.Append("|");
                blockPatterns.AppendFormat("(?'{0}'{1})", pattern.Name, pattern.GetRegexPattern());
            }
            else if (pattern is MarkupPattern)
            {
                if (markupPatterns.Length > 1)
                    markupPatterns.Append("|");
                markupPatterns.AppendFormat("(?'{0}'{1})", pattern.Name, pattern.GetRegexPattern());
            }
            else if (pattern is WordPattern)
            {
                if (wordPatterns.Length > 1)
                    wordPatterns.Append("|");
                wordPatterns.AppendFormat("(?'{0}'{1})", pattern.Name, pattern.GetRegexPattern());
            }

        if (blockPatterns.Length > 0)
            allPatterns.AppendFormat("(?'blocks'{0})+?", blockPatterns);
        if (markupPatterns.Length > 0)
            allPatterns.AppendFormat("|(?'markup'{0})+?", markupPatterns);
        if (wordPatterns.Length > 0)
            allPatterns.AppendFormat("|(?'words'{0})+?", wordPatterns);

        return allPatterns.ToString();
    }

    /// <summary>
    /// Returns the string representation of the object.
    /// </summary>
    /// <returns>
    /// The name of the object as a string.
    /// </returns>
    public override string ToString() => Name;
}