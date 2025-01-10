namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents a markup pattern that extends the base Pattern class.
/// </summary>
public sealed class MarkupPattern : Pattern
{
    /// <summary>
    /// Initializes a new instance of the MarkupPattern class.
    /// </summary>
    /// <param name="name">
    /// The name of the markup pattern.
    /// </param>
    /// <param name="style">
    /// The style of the markup pattern.
    /// </param>
    /// <param name="highlightAttributes">
    /// A boolean value indicating whether attributes should be highlighted.
    /// </param>
    /// <param name="bracketColors">
    /// The colors for brackets in the markup pattern.
    /// </param>
    /// <param name="attributeNameColors">
    /// The colors for attribute names in the markup pattern.
    /// </param>
    /// <param name="attributeValueColors">
    /// The colors for attribute values in the markup pattern.
    /// </param>
    public MarkupPattern(string name, Style style, bool highlightAttributes, ColorPair bracketColors, ColorPair attributeNameColors, ColorPair attributeValueColors)
        : base(name, style)
    {
        HighlightAttributes = highlightAttributes;
        BracketColors = bracketColors;
        AttributeNameColors = attributeNameColors;
        AttributeValueColors = attributeValueColors;
    }

    /// <summary>
    /// Gets or sets the color pair for attribute names.
    /// </summary>
    public ColorPair AttributeNameColors { get; set; }

    /// <summary>
    /// Gets or sets the color pair for attribute values.
    /// </summary>
    public ColorPair AttributeValueColors { get; set; }

    /// <summary>
    /// Gets or sets the color pair for brackets.
    /// </summary>
    public ColorPair BracketColors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to highlight attributes.
    /// </summary>
    /// <value>
    /// True if attributes should be highlighted; otherwise, false.
    /// </value>
    public bool HighlightAttributes { get; set; }

    /// <summary>
    /// Gets the regular expression pattern for matching XML tags.
    /// </summary>
    /// <returns>
    /// A string representing the regular expression pattern for matching XML tags.
    /// </returns>
    public override string GetRegexPattern()
    {
        return @"
                (?'openTag'&lt;\??/?)
                (?'ws1'\s*?)
                (?'tagName'[\w\:]+)
                (?>
                    (?!=[\/\?]?&gt;)
                    (?'ws2'\s*?)
                    (?'attribute'
                        (?'attribName'[\w\:-]+)
                        (?'attribValue'(\s*=\s*(?:&\#39;.*?&\#39;|&quot;.*?&quot;|\w+))?)
                        # (?:(?'ws3'\s*)(?'attribSign'=)(?'ws4'\s*))
                        # (?'attribValue'(?:&\#39;.*?&\#39;|&quot;.*?&quot;|\w+))
                    )
                )*
                (?'ws5'\s*?)
                (?'closeTag'[\/\?]?&gt;)
            ";
    }
}