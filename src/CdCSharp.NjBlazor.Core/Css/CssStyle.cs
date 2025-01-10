namespace CdCSharp.NjBlazor.Core.Css;

public class CssStyle
{
    public CssStyle(string selector, params CssAttribute[] cssAttributes)
    {
        Selector = selector;
        Attributes = cssAttributes;
    }

    public IEnumerable<CssAttribute> Attributes { get; init; }
    public string Selector { get; init; }
}