using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Core.Strings;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Stack;

/// <summary>
/// Represents a custom stack control that inherits from NjComponentBase.
/// </summary>
public partial class NjStack : NjComponentBase
{
    /// <summary>
    /// Gets or sets the alignment mode for the items along the cross axis.
    /// </summary>
    /// <value>
    /// The alignment mode for the items along the cross axis.
    /// </value>
    [Parameter]
    public AlignItemsMode AlignItems { get; set; } = AlignItemsMode.Start;

    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the direction of the flex container.
    /// </summary>
    /// <value>
    /// The direction of the flex container.
    /// </value>
    [Parameter]
    public FlexDirectionMode Direction { get; set; } = FlexDirectionMode.Row;

    /// <summary>
    /// Gets or sets the value for growth.
    /// </summary>
    /// <value>
    /// The value for growth.
    /// </value>
    [Parameter]
    public int Grow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content should be displayed inline.
    /// </summary>
    /// <value>
    /// True if the content should be displayed inline; otherwise, false.
    /// </value>
    [Parameter]
    public bool Inline { get; set; }

    /// <summary>
    /// Gets or sets the mode for justifying content within a container.
    /// </summary>
    /// <value>
    /// The mode for justifying content. Default value is JustifyContentMode.FlexStart.
    /// </value>
    [Parameter]
    public JustifyContentMode JustifyContent { get; set; } = JustifyContentMode.FlexStart;

    /// <summary>
    /// Gets or sets the spacing value.
    /// </summary>
    /// <value>
    /// The spacing value.
    /// </value>
    [Parameter]
    public int Spacing { get; set; }

    /// <summary>
    /// Gets or sets the wrapping mode for flex items.
    /// </summary>
    /// <value>
    /// The wrapping mode for flex items.
    /// </value>
    [Parameter]
    public FlexWrapMode Wrap { get; set; } = FlexWrapMode.Wrap;

    private string CssClass => CalculateCssClass();

    private string CalculateCssClass()
    {
        List<string> cssClasses = [];
        cssClasses.Add(CssClassReferences.Stack.Component);

        cssClasses.Add(CssTools.CalculateCssFlexClass(Inline));
        cssClasses.Add(CssTools.CalculateCssFlexDirectionClass(Direction));
        cssClasses.Add(CssTools.CalculateCssFlexWrapClass(Wrap));
        cssClasses.Add(CssTools.CalculateCssJustifyContentClass(JustifyContent));
        cssClasses.Add(CssTools.CalculateCssAlignItemsClass(AlignItems));
        cssClasses.Add(CssTools.CalculateCssGapClass(Spacing));

        if (!string.IsNullOrEmpty(Class))
        {
            cssClasses.Add(Class);
        }

        return cssClasses.NotEmptyJoin();
    }
}