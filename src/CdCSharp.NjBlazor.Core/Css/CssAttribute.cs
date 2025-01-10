namespace CdCSharp.NjBlazor.Core.Css;

/// <summary>
/// Css Attribute representation.
/// </summary>
/// <param name="name">The attribute name</param>
/// <param name="value">The attribute value</param>
public class CssAttribute(string name, string value)
{
    public string Name { get; } = name;
    public string Value { get; } = value;
}

/// <summary>
/// Css Variable representation
/// </summary>
/// <param name="prefix">The variable prefix</param>
/// <param name="name">The variable name</param>
/// <param name="value">The variable value</param>
/// <param name="type">The <see cref="CssVariableType" /></param>
public class CssVariable(string prefix, string name, string value, CssVariableType type) : CssAttribute(name, value)
{
    public string FullName => $"--{Prefix}-{Name}";
    public string Prefix { get; } = prefix;
    public CssVariableType Type { get; } = type;
}

/// <summary>
/// Css variable types used in <see cref="CssVariable" />
/// </summary>
public enum CssVariableType
{
    Size,
    Color,
    Number,
    Word,
    Shadow
}