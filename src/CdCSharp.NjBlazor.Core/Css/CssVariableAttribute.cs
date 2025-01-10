namespace CdCSharp.NjBlazor.Core.Css;

public class CssVariableAttribute : Attribute
{
    public CssVariableAttribute(CssVariableType type) => Type = type;

    public CssVariableType Type { get; }
}