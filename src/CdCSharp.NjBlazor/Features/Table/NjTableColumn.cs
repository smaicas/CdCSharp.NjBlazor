using System.Linq.Expressions;

namespace CdCSharp.NjBlazor.Features.Table;
public class NjTableColumn<T>
{
    public string Title { get; set; }
    public Expression<Func<T, object>> Property { get; set; }
    public bool Sortable { get; set; } = true;
    public bool Filterable { get; set; } = true;
    public INjCellComponent CustomComponent { get; set; }
    public string Width { get; set; }
    public string FilterPlaceholder { get; set; }
}
