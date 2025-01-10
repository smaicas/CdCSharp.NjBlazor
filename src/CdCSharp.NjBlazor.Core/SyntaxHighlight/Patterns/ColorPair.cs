using System.Drawing;

namespace CdCSharp.NjBlazor.Core.SyntaxHighlight.Patterns;

/// <summary>
/// Represents a pair of colors.
/// </summary>
public class ColorPair
{
    public ColorPair()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ColorPair class with specified foreground and background colors.
    /// </summary>
    /// <param name="foreColor">
    /// The foreground color.
    /// </param>
    /// <param name="backColor">
    /// The background color.
    /// </param>
    public ColorPair(Color foreColor, Color backColor)
    {
        ForeColor = foreColor;
        BackColor = backColor;
    }

    /// <summary>
    /// Gets or sets the background color of an element.
    /// </summary>
    /// <value>
    /// The background color of the element.
    /// </value>
    public Color BackColor { get; set; }

    /// <summary>
    /// Gets or sets the foreground color.
    /// </summary>
    public Color ForeColor { get; set; }
}