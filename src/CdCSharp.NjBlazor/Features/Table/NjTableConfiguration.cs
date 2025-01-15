namespace CdCSharp.NjBlazor.Features.Table;
public class NjTableConfiguration<T>
{
    public List<NjTableColumn<T>> Columns { get; set; } = [];
    public int PageSize { get; set; } = 10;
    public bool ShowGlobalFilter { get; set; } = true;
    public bool ShowColumnFilters { get; set; } = true;
    public bool EnableSorting { get; set; } = true;
    public bool EnablePagination { get; set; } = true;
}
