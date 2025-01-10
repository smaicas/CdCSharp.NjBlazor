namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents an abstract base class for defining patterns.
/// </summary>
public abstract class Pattern
{
    /// <summary>
    /// Initializes a new instance of the Pattern class with the specified name and style.
    /// </summary>
    /// <param name="name">
    /// The name of the pattern.
    /// </param>
    /// <param name="style">
    /// The style of the pattern.
    /// </param>
    internal Pattern(string name, Style style)
    {
        Name = name;
        Style = style;
    }

    /// <summary>
    /// Gets the name property.
    /// </summary>
    /// <value>
    /// The name of the object.
    /// </value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the style of the element.
    /// </summary>
    /// <value>
    /// The style of the element.
    /// </value>
    public Style Style { get; private set; }

    /// <summary>
    /// Gets the regular expression pattern.
    /// </summary>
    /// <returns>
    /// The regular expression pattern as a string.
    /// </returns>
    public abstract string GetRegexPattern();
}