using CdCSharp.NjBlazor.Core.Numbers;
using System.Drawing;

namespace CdCSharp.NjBlazor.Core.Abstractions.Colors;

/// <summary>
/// Representation of Color in HSV format.
/// </summary>
public class HSVColor
{
    /// <summary>
    /// The Hue value.
    /// </summary>
    public int Hue { get; set; }

    /// <summary>
    /// The Saturation value.
    /// </summary>
    public double Saturation { get; set; }

    /// <summary>
    /// The Value value.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Construct class HSVColor
    /// </summary>
    /// <param name="h">0-360</param>
    /// <param name="s">0-1</param>
    /// <param name="v">0-1</param>
    public HSVColor(int h, double s, double v)
    {
        Hue = h.EnsureRange(0, 360);
        Saturation = s.EnsureRange(0, 1);
        Value = v.EnsureRange(0, 1);
    }
    /// <summary>
    /// Returns color to RGBA
    /// </summary>
    /// <param name="alphaValue">0-255</param>
    /// <returns></returns>
    public Color ToColor(int alphaValue = 255)
    {
        double hue = Hue / 360d;
        double i = Math.Floor(hue * 6);
        double f = hue * 6 - i;
        double p = Value * (1 - Saturation);
        double q = Value * (1 - f * Saturation);
        double t = Value * (1 - (1 - f) * Saturation);
        double r = 0.0d;
        double g = 0.0d;
        double b = 0.0d;
        switch (i % 6.0d)
        {
            case 0: r = Value; g = t; b = p; break;
            case 1: r = q; g = Value; b = p; break;
            case 2: r = p; g = Value; b = t; break;
            case 3: r = p; g = q; b = Value; break;
            case 4: r = t; g = p; b = Value; break;
            case 5: r = Value; g = p; b = q; break;
        }

        return Color.FromArgb(alphaValue,
            (int)Math.Round(r * 255),
            (int)Math.Round(g * 255),
            (int)Math.Round(b * 255));
    }

    /// <summary>
    /// Returns HSVColor reprentation from a System.Drawing.Color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static HSVColor FromColor(Color color)
    {
        double max = Math.Max(Math.Max(color.R, color.G), color.B);
        double min = Math.Min(Math.Min(color.R, color.G), color.B);
        double d = max - min;
        double h = 0.0d;
        double s = max == 0.0d ? 0.0d : d / max;
        double v = max / 255.0d;

        if (max == min)
            h = 0.0d;
        else if (max == color.R)
        {
            h = color.G - color.B + d * (color.G < color.B ? 6 : 0); h /= 6 * d;
        }
        else if (max == color.G)
        {

            h = color.B - color.R + d * 2; h /= 6 * d;
        }
        else if (max == color.B)
        {

            h = color.R - color.G + d * 4; h /= 6 * d;
        }
        return new HSVColor((int)(h * 360), s, v);
    }

}