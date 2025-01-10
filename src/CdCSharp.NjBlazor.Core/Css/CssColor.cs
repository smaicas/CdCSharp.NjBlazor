using CdCSharp.NjBlazor.Core.Numbers;
using System.Drawing;
using System.Globalization;

namespace CdCSharp.NjBlazor.Core.Css;

/// <summary>
/// The <see cref="CssColor" /> output formats.
/// </summary>
public enum ColorOutputFormats
{
    /// <summary>
    /// Output will be starting with a # and include r,g and b but no alpha values. Example #ab2a3d
    /// </summary>
    Hex,

    /// <summary>
    /// Output will be starting with a # and include r,g and b and alpha values. Example #ab2a3dff
    /// </summary>
    HexA,

    /// <summary>
    /// Will output css like output for value. Example rgb(12,15,40)
    /// </summary>
    Rgb,

    /// <summary>
    /// Will output css like output for value with alpha. Example rgba(12,15,40,0.42)
    /// </summary>
    Rgba,

    /// <summary>
    /// Will output the color elements without any decorator and without alpha. Example 12,15,26
    /// </summary>
    ColorElements
}

/// <summary>
/// The Css color representation.
/// </summary>
public class CssColor : IEquatable<CssColor>
{
    #region Fields and Properties

    private const double Epsilon = 0.000000000000001;
    private const double VariantModifier = 0.050;

    private readonly ColorVariant? _associatedColorVariant;
    private readonly byte[] _valuesAsByte;

    /// <summary>
    /// The Alpha value.
    /// </summary>
    public byte A => _valuesAsByte[3];

    /// <summary>
    /// The Alpha value percentage.
    /// </summary>
    public double APercentage => Math.Round(A / 255.0, 2);

    /// <summary>
    /// The Blue value.
    /// </summary>
    public byte B => _valuesAsByte[2];

    /// <summary>
    /// The Green Value.
    /// </summary>
    public byte G => _valuesAsByte[1];

    /// <summary>
    /// The Hue Value.
    /// </summary>
    public double H { get; private set; }

    /// <summary>
    /// The Luminosity Value.
    /// </summary>
    public double L { get; private set; }

    /// <summary>
    /// The Red Value.
    /// </summary>
    public byte R => _valuesAsByte[0];

    /// <summary>
    /// The Saturation Value.
    /// </summary>
    public double S { get; private set; }

    /// <summary>
    /// The Color Value.
    /// </summary>
    public string Value => $"#{R:x2}{G:x2}{B:x2}{A:x2}";

    /// <summary>
    /// Gets the alpha percentage as string.
    /// </summary>
    /// <param name="floatCharacter"></param>
    /// <returns></returns>
    public string APercentageString(char? floatCharacter = '.') =>
        APercentage
            .ToString(CultureInfo.InvariantCulture)
            .Replace(',', floatCharacter!.Value);

    #endregion Fields and Properties

    #region Constructor

    /// <summary>
    /// Constructs a CssColor from hsl and alpha values
    /// </summary>
    /// <param name="h">The hue value</param>
    /// <param name="s">The saturation value</param>
    /// <param name="l">The luminosity value</param>
    /// <param name="a">The alpha value</param>
    public CssColor(double h, double s, double l, double a)
        : this(h, s, l, (int)(a * 255.0).EnsureRange(255))
    {
    }

    /// <summary>
    /// Constructs a CssColor from hsl and alpha values
    /// </summary>
    /// <param name="h">The hue value</param>
    /// <param name="s">The saturation value</param>
    /// <param name="l">The luminosity value</param>
    /// <param name="a">The alpha value</param>
    public CssColor(double h, double s, double l, int a)
    {
        _valuesAsByte = new byte[4];

        h = Math.Round(h.EnsureRange(360), 0);
        s = Math.Round(s.EnsureRange(1), 2);
        l = Math.Round(l.EnsureRange(1), 2);
        a = a.EnsureRange(255);

        // achromatic argb (gray scale)
        if (Math.Abs(s) < Epsilon)
        {
            _valuesAsByte[0] = (byte)((int)Math.Ceiling(l * 255D)).EnsureRange(0, 255);
            _valuesAsByte[1] = (byte)((int)Math.Ceiling(l * 255D)).EnsureRange(0, 255);
            _valuesAsByte[2] = (byte)((int)Math.Ceiling(l * 255D)).EnsureRange(0, 255);
            _valuesAsByte[3] = (byte)a;
        }
        else
        {
            double q = l < .5D
                ? l * (1D + s)
                : l + s - l * s;
            double p = 2D * l - q;

            double hk = h / 360D;
            double[] T = new double[3];
            T[0] = hk + 1D / 3D; // Tr
            T[1] = hk; // Tb
            T[2] = hk - 1D / 3D; // Tg

            for (int i = 0; i < 3; i++)
            {
                if (T[i] < 0D) T[i] += 1D;
                if (T[i] > 1D) T[i] -= 1D;

                if (T[i] * 6D < 1D)
                    T[i] = p + (q - p) * 6D * T[i];
                else if (T[i] * 2D < 1)
                    T[i] = q;
                else if (T[i] * 3D < 2)
                    T[i] = p + (q - p) * (2D / 3D - T[i]) * 6D;
                else
                    T[i] = p;
            }

            _valuesAsByte[0] = ((int)Math.Round(T[0] * 255D)).EnsureRangeToByte();
            _valuesAsByte[1] = ((int)Math.Round(T[1] * 255D)).EnsureRangeToByte();
            _valuesAsByte[2] = ((int)Math.Round(T[2] * 255D)).EnsureRangeToByte();
            _valuesAsByte[3] = (byte)a;
        }

        H = Math.Round(h, 0);
        S = Math.Round(s, 2);
        L = Math.Round(l, 2);
    }

    /// <summary>
    /// Constructs a CssColor from rgba values
    /// </summary>
    /// <param name="r">The red value</param>
    /// <param name="g">The green value</param>
    /// <param name="b">The blue value</param>
    /// <param name="a">The alpha value</param>
    public CssColor(byte r, byte g, byte b, byte a)
    {
        _valuesAsByte = new byte[4];

        _valuesAsByte[0] = r;
        _valuesAsByte[1] = g;
        _valuesAsByte[2] = b;
        _valuesAsByte[3] = a;

        CalculateHsl();
    }

    /// <summary>
    /// Constructs a CssColor from rgba values
    /// </summary>
    /// <param name="r">The red value</param>
    /// <param name="g">The green value</param>
    /// <param name="b">The blue value</param>
    /// <param name="a">The alpha value</param>
    public CssColor(int r, int g, int b, double alpha) :
        this(r, g, b, (byte)(alpha * 255.0).EnsureRange(255))
    {
    }

    /// <summary>
    /// Constructs a CssColor from rgba values
    /// </summary>
    /// <param name="r">The red value</param>
    /// <param name="g">The green value</param>
    /// <param name="b">The blue value</param>
    /// <param name="a">The alpha value</param>
    public CssColor(int r, int g, int b, int alpha) :
        this((byte)r.EnsureRange(255), (byte)g.EnsureRange(255), (byte)b.EnsureRange(255), (byte)alpha.EnsureRange(255))
    {
    }

    /// <summary>
    /// Constructs a CssColor from a System.Drawing.Color value and a <see cref="ColorVariant" />
    /// </summary>
    /// <param name="color">The Color value</param>
    /// <param name="colorVariant">The Color variant</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public CssColor(Color color, ColorVariant? colorVariant = ColorVariant.Default)
    {
        _valuesAsByte = new byte[4];

        CssColor currentColor = new(color.R, color.G, color.B, color.A);
        switch (colorVariant)
        {
            case ColorVariant.Darken1:
                currentColor = currentColor.ColorDarken(VariantModifier);
                break;

            case ColorVariant.Darken2:
                currentColor = currentColor.ColorDarken(VariantModifier * 2);
                break;

            case ColorVariant.Darken3:
                currentColor = currentColor.ColorDarken(VariantModifier * 3);
                break;

            case ColorVariant.Darken4:
                currentColor = currentColor.ColorDarken(VariantModifier * 4);
                break;

            case ColorVariant.Darken5:
                currentColor = currentColor.ColorDarken(VariantModifier * 5);
                break;

            case ColorVariant.Lighten1:
                currentColor = currentColor.ColorLighten(VariantModifier);
                break;

            case ColorVariant.Lighten2:
                currentColor = currentColor.ColorLighten(VariantModifier * 2);
                break;

            case ColorVariant.Lighten3:
                currentColor = currentColor.ColorLighten(VariantModifier * 3);
                break;

            case ColorVariant.Lighten4:
                currentColor = currentColor.ColorLighten(VariantModifier * 4);
                break;

            case ColorVariant.Lighten5:
                currentColor = currentColor.ColorLighten(VariantModifier * 5);
                break;

            case ColorVariant.Default:
            case null:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(colorVariant), colorVariant, null);
        }

        _valuesAsByte[0] = currentColor.R;
        _valuesAsByte[1] = currentColor.G;
        _valuesAsByte[2] = currentColor.B;
        _valuesAsByte[3] = currentColor.A;

        CalculateHsl();
    }

    /// <summary>
    /// Constructs a CssColor from string representation.
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException"></exception>
    public CssColor(string value)
    {
        value = value.Trim().ToLower();

        if (value.StartsWith("rgba"))
        {
            string[] parts = SplitInputIntoParts(value);
            if (parts.Length != 4) throw new ArgumentException("invalid color format");

            _valuesAsByte = new[]
            {
                byte.Parse(parts[0], CultureInfo.InvariantCulture),
                byte.Parse(parts[1], CultureInfo.InvariantCulture),
                byte.Parse(parts[2], CultureInfo.InvariantCulture),
                (byte)(255 * double.Parse(parts[3], CultureInfo.InvariantCulture)).EnsureRange(0,255)
            };
        }
        else if (value.StartsWith("rgb"))
        {
            string[] parts = SplitInputIntoParts(value);
            if (parts.Length != 3) throw new ArgumentException("invalid color format");
            _valuesAsByte = new byte[]
            {
                byte.Parse(parts[0], CultureInfo.InvariantCulture),
                byte.Parse(parts[1], CultureInfo.InvariantCulture),
                byte.Parse(parts[2], CultureInfo.InvariantCulture),
                255
            };
        }
        else
        {
            if (value.StartsWith('#')) value = value[1..];

            switch (value.Length)
            {
                case 3:
                    value = new string(new[]
                        { value[0], value[0], value[1], value[1], value[2], value[2], 'F', 'F' });
                    break;

                case 4:
                    value = new string(new[]
                        { value[0], value[0], value[1], value[1], value[2], value[2], value[3], value[3] });
                    break;

                case 6:
                    value += "FF";
                    break;

                case 8:
                    break;

                default:
                    throw new ArgumentException("not a valid color", nameof(value));
            }

            _valuesAsByte = new[]
            {
                GetByteFromValuePart(value, 0),
                GetByteFromValuePart(value, 2),
                GetByteFromValuePart(value, 4),
                GetByteFromValuePart(value, 6)
            };

            CalculateHsl();
        }
    }

    #endregion Constructor

    #region Methods

    /// <summary>
    /// Modify Lightness value.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public CssColor ChangeLightness(double amount) => new(H, S, (L + amount).EnsureRange(0, 1), A);

    /// <summary>
    /// Get darken color from current instance.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public CssColor ColorDarken(double amount) => ChangeLightness(-amount);

    /// <summary>
    /// Get lighten color from current instance.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public CssColor ColorLighten(double amount) => ChangeLightness(+amount);

    /// <summary>
    /// Sets the alpha value for current instance.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public CssColor SetAlpha(int a) => new(R, G, B, a);

    /// <summary>
    /// Sets the alpha value for current instance.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public CssColor SetAlpha(double a) => new(R, G, B, a);

    private void CalculateHsl()
    {
        double h = 0D;
        double s = 0D;
        double l;

        // normalize red, green, blue values
        double r = R / 255D;
        double g = G / 255D;
        double b = B / 255D;

        double max = Math.Max(r, Math.Max(g, b));
        double min = Math.Min(r, Math.Min(g, b));

        // hue
        if (Math.Abs(max - min) < Epsilon)
            h = 0D; // undefined
        else if (Math.Abs(max - r) < Epsilon
                 && g >= b)
            h = 60D * (g - b) / (max - min);
        else if (Math.Abs(max - r) < Epsilon
                 && g < b)
            h = 60D * (g - b) / (max - min) + 360D;
        else if (Math.Abs(max - g) < Epsilon)
            h = 60D * (b - r) / (max - min) + 120D;
        else if (Math.Abs(max - b) < Epsilon) h = 60D * (r - g) / (max - min) + 240D;

        // luminance
        l = (max + min) / 2D;

        // saturation
        if (Math.Abs(l) < Epsilon
            || Math.Abs(max - min) < Epsilon)
            s = 0D;
        else if (l is > 0D and <= .5D)
            s = (max - min) / (max + min);
        else if (l > .5D) s = (max - min) / (2D - (max + min)); //(max-min > 0)?

        H = Math.Round(h.EnsureRange(360), 0);
        S = Math.Round(s.EnsureRange(1), 2);
        L = Math.Round(l.EnsureRange(1), 2);
    }

    #endregion Methods

    #region Helper

    private static byte GetByteFromValuePart(string input, int index) => byte.Parse(new string(new[] { input[index], input[index + 1] }), NumberStyles.HexNumber);

    private static string[] SplitInputIntoParts(string value)
    {
        int startIndex = value.IndexOf('(');
        int lastIndex = value.LastIndexOf(')');
        string subString = value[(startIndex + 1)..lastIndex];
        string[] parts = subString.Split(',', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < parts.Length; i++)
            parts[i] = parts[i].Trim();

        return parts;
    }

    #endregion Helper

    #region operators and object members

    public static explicit operator string(CssColor? color)
    {
        return color == null ? string.Empty : color.Value;
    }

    public static implicit operator CssColor(string input)
    {
        return new(input);
    }

    public static bool operator !=(CssColor? lhs, CssColor? rhs)
    {
        return !(lhs == rhs);
    }

    public static bool operator ==(CssColor? lhs, CssColor? rhs)
    {
        bool lhsIsNull = lhs is null;
        bool rhsIsNull = rhs is null;
        if (lhsIsNull && rhsIsNull) return true;

        if (lhsIsNull || rhsIsNull) return false;
        return lhs!.Equals(rhs!);
    }

    public override bool Equals(object? obj) => obj is CssColor color && Equals(color);

    public bool Equals(CssColor? other)
    {
        if (other is null) return false;

        return
            _valuesAsByte[0] == other._valuesAsByte[0] &&
            _valuesAsByte[1] == other._valuesAsByte[1] &&
            _valuesAsByte[2] == other._valuesAsByte[2] &&
            _valuesAsByte[3] == other._valuesAsByte[3];
    }

    public override int GetHashCode() => _valuesAsByte[0] + _valuesAsByte[1] + _valuesAsByte[2] + _valuesAsByte[3];

    public override string ToString() => ToString(ColorOutputFormats.Rgba);

    public string ToString(ColorOutputFormats format)
    {
        return format switch
        {
            ColorOutputFormats.Hex => Value.Substring(0, 7),
            ColorOutputFormats.HexA => Value,
            ColorOutputFormats.Rgb => $"rgb({R},{G},{B})",
            ColorOutputFormats.Rgba => $"rgba({R},{G},{B},{(A / 255.0).ToString(CultureInfo.InvariantCulture)})",
            ColorOutputFormats.ColorElements => $"{R},{G},{B}",
            _ => Value
        };
    }

    #endregion operators and object members
}