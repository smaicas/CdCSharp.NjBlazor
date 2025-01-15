namespace CdCSharp.NjBlazor.Features.Table;

public class NjTableConfiguration<T>
{
    public List<NjTableColumn<T>> Columns { get; set; } = [];
    public bool EnablePagination { get; set; } = true;
    public bool EnableSorting { get; set; } = true;
    public int PageSize { get; set; } = 10;
    public bool ShowColumnFilters { get; set; } = true;
    public bool ShowGlobalFilter { get; set; } = true;
}