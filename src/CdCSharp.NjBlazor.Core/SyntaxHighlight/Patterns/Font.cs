namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents a font used for text rendering.
/// </summary>
public class Font
{
    /// <summary>
    /// Initializes a new instance of the Font class with the specified name, size, and style.
    /// </summary>
    /// <param name="name">
    /// The name of the font.
    /// </param>
    /// <param name="size">
    /// The size of the font.
    /// </param>
    /// <param name="style">
    /// The style of the font.
    /// </param>
    public Font(string name, float size, FontStyle style)
    {
        Name = name;
        Size = size;
        Style = style;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the size of an object.
    /// </summary>
    /// <value>
    /// The size of the object.
    /// </value>
    public float Size { get; set; }

    /// <summary>
    /// Gets or sets the font style.
    /// </summary>
    public FontStyle Style { get; set; }
}