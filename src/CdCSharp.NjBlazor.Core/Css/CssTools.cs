using CdCSharp.NjBlazor.Core.Abstractions.Css;
using System.Globalization;

namespace CdCSharp.NjBlazor.Core.Css;

public static class CssTools
{
    public static string CalculateCssAlignItemsClass(AlignItemsMode alignItemsMode)
    {
        return alignItemsMode switch
        {
            AlignItemsMode.Start => CssClassReferences.AlignItems.AlignItemsStart,
            AlignItemsMode.End => CssClassReferences.AlignItems.AlignItemsEnd,
            AlignItemsMode.Center => CssClassReferences.AlignItems.AlignItemsCenter,
            AlignItemsMode.Stretch => CssClassReferences.AlignItems.AlignItemsStretch,
            AlignItemsMode.Baseline => CssClassReferences.AlignItems.AlignItemsBaseline,
            _ => throw new ArgumentOutOfRangeException(nameof(AlignItemsMode)),
        };
    }

    public static string CalculateCssBorderValue(BorderStyleMode borderStyle, int borderWidth, int borderRadius, CssColor? color)
        => $"border-width:{borderWidth}px; " +
            $"border-radius:{borderRadius.ToString()}px; " +
            $"border-color: {(color != null ? color.ToString(ColorOutputFormats.Rgba) : "")}; " +
            $"border-style: {borderStyle.ToString().ToLower()}";

    public static string CalculateCssFlexClass(bool inline)
    {
        return inline ?
            CssClassReferences.FlexBox.InlineFlex
            : CssClassReferences.FlexBox.Flex;
    }

    public static string CalculateCssFlexDirectionClass(FlexDirectionMode flexDirectionMode)
    {
        return flexDirectionMode switch
        {
            FlexDirectionMode.Column => CssClassReferences.FlexBox.Column,
            FlexDirectionMode.Row => CssClassReferences.FlexBox.Row,
            FlexDirectionMode.RowReverse => CssClassReferences.FlexBox.RowReverse,
            FlexDirectionMode.ColumnReverse => CssClassReferences.FlexBox.ColumnReverse,
            _ => throw new ArgumentOutOfRangeException(nameof(FlexDirectionMode)),
        };
    }

    public static string CalculateCssFlexWrapClass(FlexWrapMode flexWrapMode)
    {
        return flexWrapMode switch
        {
            FlexWrapMode.Wrap => CssClassReferences.FlexBox.FlexWrapWrap,
            FlexWrapMode.NoWrap => CssClassReferences.FlexBox.FlexWrapNoWrap,
            FlexWrapMode.WrapReverse => CssClassReferences.FlexBox.FlexWrapWrapReverse,
            _ => throw new ArgumentOutOfRangeException(nameof(FlexWrapMode)),
        };
    }

    public static string CalculateCssGapClass(int spacing)
    {
        spacing = Math.Clamp(spacing, 0, CssClassReferences.MaxGapValue);
        return $"{CssClassReferences.Prefix.Gap}{spacing}";
    }

    public static string CalculateCssJustifyContentClass(JustifyContentMode justifyContentMode)
    {
        return justifyContentMode switch
        {
            JustifyContentMode.FlexStart => CssClassReferences.JustifyContent.JustifyContentStart,
            JustifyContentMode.FlexEnd => CssClassReferences.JustifyContent.JustifyContentEnd,
            JustifyContentMode.Center => CssClassReferences.JustifyContent.JustifyContentCenter,
            JustifyContentMode.SpaceBetween => CssClassReferences.JustifyContent.JustifyContentSpaceBetween,
            JustifyContentMode.SpaceAround => CssClassReferences.JustifyContent.JustifyContentSpaceAround,
            JustifyContentMode.SpaceEvenly => CssClassReferences.JustifyContent.JustifyContentSpaceEvenly,
            _ => throw new ArgumentOutOfRangeException(nameof(JustifyContentMode)),
        };
    }

    public static string CalculateCssJustifyItemsClass(JustifyItemsMode justifyItemsMode)
    {
        return justifyItemsMode switch
        {
            JustifyItemsMode.FlexStart => CssClassReferences.JustifyItems.JustifyItemsStart,
            JustifyItemsMode.FlexEnd => CssClassReferences.JustifyItems.JustifyItemsEnd,
            JustifyItemsMode.Center => CssClassReferences.JustifyItems.JustifyItemsCenter,
            JustifyItemsMode.Stretch => CssClassReferences.JustifyItems.JustifyItemsStretch,
            JustifyItemsMode.Normal => CssClassReferences.JustifyItems.JustifyItemsNormal,
            _ => throw new ArgumentOutOfRangeException(nameof(JustifyItemsMode)),
        };
    }

    public static string CalculateCssTreePaddingValue(int level) => $"{level * 4}px";

    public static string CalculateCssPaddingClass(int padding)
    {
        padding = Math.Clamp(padding, 0, CssClassReferences.MaxPaddingValue);

        return $"{CssClassReferences.Prefix.Padding}{padding}";
    }

    public static string CalculateCssShadowValue(bool inset, int offsetX, int offsetY, int blurRadius, int spreadRadius, CssColor? color)
        => $"box-shadow: {(inset ? "inset " : string.Empty)}{offsetX}px {offsetY}px {blurRadius}px {spreadRadius}px {(color != null ? color.ToString(ColorOutputFormats.Rgba) : "")};";
    public static string ToCssNumber(double value) =>
        Math.Round(value, 1).ToString(CultureInfo.InvariantCulture).Replace(",", ".");
}