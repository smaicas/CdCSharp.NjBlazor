namespace CdCSharp.NjBlazor.Features.TextPattern.Abstractions;

public sealed class ElementPattern
{
    /// <summary>Gets or sets the pattern.</summary>
    /// <value>The pattern string.</value>
    public string Pattern { get; set; }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The current value of the property.</value>
    public string Value { get; set; }

    /// <summary>Gets or sets the length property.</summary>
    public int Length { get; set; }

    /// <summary>Gets or sets the default value.</summary>
    /// <value>The default value.</value>
    public string DefaultValue { get; set; }

    /// <summary>Gets or sets a value indicating whether the item is a separator.</summary>
    /// <value>True if the item is a separator; otherwise, false.</value>
    public bool IsSeparator { get; set; }

    /// <summary>Gets or sets a value indicating whether the object is editable.</summary>
    /// <value>True if the object is editable; otherwise, false.</value>
    public bool IsEditable { get; set; }

    /// <summary>Initializes a new instance of the ElementPattern class.</summary>
    /// <param name="pattern">The pattern of the element.</param>
    /// <param name="value">The value of the element.</param>
    /// <param name="length">The length of the element.</param>
    /// <param name="defaultValue">The default value of the element.</param>
    /// <param name="isSeparator">A boolean indicating if the element is a separator.</param>
    /// <param name="isEditable">A boolean indicating if the element is editable.</param>
    public ElementPattern(
        string pattern,
        string value,
        int length,
        string defaultValue,
        bool isSeparator,
        bool isEditable
    )
    {
        Pattern = pattern;
        Value = value;
        Length = length;
        DefaultValue = defaultValue;
        IsSeparator = isSeparator;
        IsEditable = isEditable;
    }
}
