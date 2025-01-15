namespace CdCSharp.NjBlazor.Core;

/// <summary>
/// Represents a collection of CSS class references.
/// </summary>
public class CssClassReferences
{
    public const string Active = "active";
    public const string Disabled = "disabled";
    public const string Empty = "empty";
    public const string Focus = "focus";
    public const string FormControl = "form-control";
    public const string Hidden = "hidden";
    public const int MaxGapValue = 10;
    public const int MaxPaddingValue = 10;
    public const string Open = "open";
    public const string Pointer = "pointer";

    public class AlignItems
    {
        public const string AlignItemsBaseline = "nj-flex-align-items-baseline";
        public const string AlignItemsCenter = "nj-flex-align-items-center";
        public const string AlignItemsEnd = "nj-flex-align-items-end";
        public const string AlignItemsStart = "nj-flex-align-items-start";
        public const string AlignItemsStretch = "nj-flex-align-items-stretch";
    }

    public class AlignSelf
    {
        public const string AlignSelfAuto = "nj-align-self-auto";
        public const string AlignSelfBaseline = "nj-align-self-baseline";
        public const string AlignSelfCenter = "nj-align-self-center";
        public const string AlignSelfEnd = "nj-align-self-end";
        public const string AlignSelfStart = "nj-align-self-start";
        public const string AlignSelfStretch = "nj-align-self-stretch";
    }

    public class Color
    {
        public const string Primary = "nj-color-primary";
    }

    public class FlexBox
    {
        public const string Column = "column";
        public const string ColumnReverse = "column-reverse";
        public const string Flex = "nj-flex";
        public const string FlexWrapNoWrap = "nj-flex-wrap-nowrap";
        public const string FlexWrapWrap = "nj-flex-wrap-wrap";
        public const string FlexWrapWrapReverse = "nj-flex-wrap-wrap-reverse";
        public const string InlineFlex = "nj-flex-inline";
        public const string Row = "row";
        public const string RowReverse = "row-reverse";
    }

    public class Icon
    {
        public const string IconSizeLarge = "nj-icon-size-large";
        public const string IconSizeMedium = "nj-icon-size-medium";
        public const string IconSizeSmall = "nj-icon-size-small";
        public const string IconSizeXLarge = "nj-icon-size-xlarge";
        public const string IconSizeXXLarge = "nj-icon-size-xxlarge";
    }

    public class JustifyContent
    {
        public const string JustifyContentCenter = "nj-justify-content-center";
        public const string JustifyContentEnd = "nj-justify-content-end";
        public const string JustifyContentSpaceAround = "nj-justify-content-space-around";
        public const string JustifyContentSpaceBetween = "nj-justify-content-space-between";
        public const string JustifyContentSpaceEvenly = "nj-justify-content-space-evenly";
        public const string JustifyContentStart = "nj-justify-content-start";
    }

    public class JustifyItems
    {
        public const string JustifyItemsCenter = "nj-flex-justify-items-center";
        public const string JustifyItemsEnd = "nj-flex-justify-items-end";
        public const string JustifyItemsNormal = "nj-flex-justify-items-normal";
        public const string JustifyItemsStart = "nj-flex-justify-items-start";
        public const string JustifyItemsStretch = "nj-flex-justify-items-stretch";
    }

    public class Position
    {
        public const string Absolute = "nj-position-absolute";
        public const string Fixed = "nj-position-fixed";
        public const string Relative = "nj-position-relative";
        public const string Static = "nj-position-static";
        public const string Sticky = "nj-position-sticky";
    }

    public class Prefix
    {
        public const string Background = "nj-bg-";
        public const string Color = "nj-color-";
        public const string Gap = "nj-gap-";
        public const string Margin = "nj-m-";
        public const string MarginX = "nj-mx-";
        public const string MarginY = "nj-my-";
        public const string Padding = "nj-p-";
        public const string PaddingX = "nj-px-";
        public const string PaddingY = "nj-py-";
    }

    public class Stack
    {
        public const string Component = "nj-stack";
    }

    public class Tabs
    {
        public const string TabsColumn = "nj-tabs-column";
        public const string TabsColumnReverse = "nj-tabs-column-reverse";
        public const string TabsRoot = "nj-tabs";
        public const string TabsRow = "nj-tabs-row";
        public const string TabsRowReverse = "nj-tabs-row-reverse";
    }

    public class Text
    {
        public const string AlignCenter = "nj-text-center";
        public const string AlignRight = "nj-text-right";
        public const string Bold = "nj-text-bold";
        public const string Italic = "nj-text-italic";
        public const string Password = "nj-password-like";
        public const string TextArea = "nj-textarea-like";
        public const string Underline = "nj-text-underline";
    }
}