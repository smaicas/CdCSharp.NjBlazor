namespace CdCSharp.NjBlazor.Features.ColorPicker.Components;

/// <summary>
/// Specifies the output format for a color picker.
/// </summary>
public enum ColorpickerOutputFormat
{
    RGB = 1 << 0,
    HSV = 1 << 1,
    HEX = 1 << 2,
}
