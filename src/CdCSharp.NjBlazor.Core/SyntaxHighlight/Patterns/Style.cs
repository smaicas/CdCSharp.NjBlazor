namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents the style of an element in a user interface. This class can be used to define visual
/// properties such as colors, fonts, and layout.
/// </summary>
public class Style
{
    /// <summary>
    /// Initializes a new instance of the Style class with specified colors and font.
    /// </summary>
    /// <param name="colors">
    /// The color pair for the style.
    /// </param>
    /// <param name="font">
    /// The font for the style.
    /// </param>
    public Style(ColorPair colors, Font font)
    {
        Colors = colors;
        Font = font;
    }

    /// <summary>
    /// Gets or sets the color pair.
    /// </summary>
    /// <value>
    /// The color pair.
    /// </value>
    public ColorPair Colors { get; private set; }

    /// <summary>
    /// Gets or sets the font used for rendering text.
    /// </summary>
    public Font Font { get; private set; }
}