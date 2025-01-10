using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Grid;

/// <summary>
/// Represents a grid cell in the NjGrid component.
/// </summary>
public partial class NjGridCell : NjComponentBase
{
    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>Gets or sets the column number.</summary>
    /// <value>The column number. Can be null.</value>
    [Parameter]
    public int? Column { get; set; }

    /// <summary>Gets or sets the number of columns that the element spans within a grid.</summary>
    /// <value>The number of columns spanned by the element. Default value is 1.</value>
    [Parameter]
    public int ColumnSpan { get; set; } = 1;

    /// <summary>Gets or sets the row number.</summary>
    /// <value>The row number. Can be null.</value>
    [Parameter]
    public int? Row { get; set; }

    /// <summary>Gets or sets the number of rows that a cell spans within a grid.</summary>
    /// <value>The number of rows that the cell spans. The default value is 1.</value>
    [Parameter]
    public int RowSpan { get; set; } = 1;

    /// <summary>
    /// Gets or sets the position mode.
    /// </summary>
    /// <value>
    /// The position mode.
    /// </value>
    [Parameter]
    public PositionMode Position { get; set; } = PositionMode.Static;

    /// <summary>Gets or sets the parent NjGrid component in a cascading parameter.</summary>
    /// <value>The parent NjGrid component.</value>
    [CascadingParameter]
    public NjGrid? ParentGrid { get; set; }

    /// <summary>
    /// This method is called when the parameters are set.
    /// It adds the current cell to the parent grid if the parent grid is not null.
    /// </summary>
    protected override void OnParametersSet()
    {
        if (ParentGrid == null)
            return;
        ParentGrid.AddCell(this);
    }
}
