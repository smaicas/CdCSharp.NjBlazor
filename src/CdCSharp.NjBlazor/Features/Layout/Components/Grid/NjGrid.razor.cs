using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Core.Strings;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Grid;

/// <summary>
/// Represents a custom grid control.
/// </summary>
public partial class NjGrid
{
    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>Gets or sets the template columns mode.</summary>
    /// <value>The template columns mode.</value>
    [Parameter]
    public TemplateColumnsMode TemplateColumns { get; set; } = TemplateColumnsMode.Fit;

    private string _templateColumnsSpecificStyle =>
        TemplateColumnsSpecific != null
            ? $"grid-template-columns:{TemplateColumnsSpecific}"
            : string.Empty;

    /// <summary>
    /// Gets or sets the specific template columns for the control.
    /// </summary>
    /// <value>
    /// A string representing the specific template columns for the control.
    /// </value>
    [Parameter]
    public string? TemplateColumnsSpecific { get; set; }

    /// <summary>Gets or sets the mode for justifying items within a container.</summary>
    /// <value>The mode for justifying items.</value>
    [Parameter]
    public JustifyItemsMode JustifyItems { get; set; } = JustifyItemsMode.Normal;

    /// <summary>
    /// Gets or sets the alignment mode for the items along the cross axis.
    /// </summary>
    /// <value>
    /// The alignment mode for the items along the cross axis.
    /// </value>
    [Parameter]
    public AlignItemsMode AlignItems { get; set; } = AlignItemsMode.Center;

    private readonly List<NjGridCell> _gridCells = [];

    string TemplateColumnsClass => TemplateColumns.Equals(TemplateColumnsMode.Fit) ? "fit" : "fill";
    private string CssClass => GetCssClass();
    Dictionary<NjGridCell, string> _styles => GetGridStyles();
    Dictionary<NjGridCell, string> _classes => GetGridCssClasses();

    private string GetCssClass()
    {
        List<string> cssClasses = [];
        cssClasses.Add("nj-grid");
        if (string.IsNullOrEmpty(TemplateColumnsSpecific))
        {
            cssClasses.Add(TemplateColumns.Equals(TemplateColumnsMode.Fit) ? "fit" : "fill");
        }

        cssClasses.Add(CssTools.CalculateCssJustifyItemsClass(JustifyItems));
        cssClasses.Add(CssTools.CalculateCssAlignItemsClass(AlignItems));

        return cssClasses.NotEmptyJoin();
    }

    /// <summary>Adds a grid cell to the collection of grid cells.</summary>
    /// <param name="gridCell">The grid cell to add.</param>
    /// <remarks>
    /// If the grid cell is already present in the collection, it will not be added again.
    /// After adding the grid cell, invokes a state change asynchronously.
    /// </remarks>
    public void AddCell(NjGridCell gridCell)
    {
        if (_gridCells.Contains(gridCell))
            return;
        _gridCells.Add(gridCell);
        StateHasChanged();
    }

    private Dictionary<NjGridCell, string> GetGridStyles()
    {
        Dictionary<NjGridCell, string> styles = [];
        foreach (NjGridCell gridCell in _gridCells)
        {
            string style =
                $"grid-column:{gridCell.Column?.ToString() ?? "auto"} / span {gridCell.ColumnSpan};grid-row: {gridCell.Row?.ToString() ?? "auto"} / span {gridCell.RowSpan}";

            styles.Add(gridCell, style);
        }
        return styles;
    }

    private Dictionary<NjGridCell, string> GetGridCssClasses()
    {
        Dictionary<NjGridCell, string> classes = [];

        foreach (NjGridCell gridCell in _gridCells)
        {
            classes.Add(gridCell, GetGridCssClass(gridCell));
        }
        return classes;
    }

    private string GetGridCssClass(NjGridCell gridCell)
    {
        List<string> cssClasses = ["nj-grid-cell", gridCell.Class];

        switch (gridCell.Position)
        {
            default:
            case PositionMode.Static:
                cssClasses.Add(CssClassReferences.Position.Static);
                break;
            case PositionMode.Relative:
                cssClasses.Add(CssClassReferences.Position.Relative);
                break;
            case PositionMode.Absolute:
                cssClasses.Add(CssClassReferences.Position.Absolute);
                break;
            case PositionMode.Fixed:
                cssClasses.Add(CssClassReferences.Position.Fixed);
                break;
            case PositionMode.Sticky:
                cssClasses.Add(CssClassReferences.Position.Sticky);
                break;
        }

        return cssClasses.NotEmptyJoin();
    }
}
