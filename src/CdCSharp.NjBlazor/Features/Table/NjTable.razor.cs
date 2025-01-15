using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace CdCSharp.NjBlazor.Features.Table;
public partial class NjTable<T> : NjComponentBase
{
    [Parameter]
    public IEnumerable<T> Items { get; set; }

    [Parameter]
    public NjTableConfiguration<T> Configuration { get; set; }

    private IEnumerable<T> _filteredItems;
    private string _globalFilter = string.Empty;
    private Dictionary<string, string> _columnFilters = [];
    private string _sortColumn;
    private bool _sortAscending = true;
    private int _currentPage = 1;

    protected override void OnInitialized()
    {
        _filteredItems = Items;

        IEnumerable<NjTableColumn<T>> columns = Configuration.Columns.OfType<NjTableColumn<T>>();

        IEnumerable<string> columnNames = columns.Select(c => c.GetColumnKey());

        _columnFilters = columnNames.ToDictionary(colName => colName, x => string.Empty);

        ApplyFiltersAndSort();
    }

    private void ApplyFiltersAndSort()
    {
        IQueryable<T> query = Items.AsQueryable();

        // Aplicar filtro global
        if (!string.IsNullOrWhiteSpace(_globalFilter))
        {
            query = query.Where(item =>
                Configuration.Columns.Any(col =>
                    GetPropertyValue(item, col.Property).ToString()
                        .Contains(_globalFilter, StringComparison.OrdinalIgnoreCase)));
        }

        // Aplicar filtros de columna
        for (int i = 0; i < Configuration.Columns.Count; i++)
        {
            NjTableColumn<T> column = Configuration.Columns[i];
            string columnKey = column.GetColumnKey();

            if (_columnFilters.TryGetValue(columnKey, out string? filterValue) &&
                !string.IsNullOrWhiteSpace(filterValue))
            {
                query = query.Where(item =>
                    GetPropertyValue(item, column.Property).ToString()
                        .Contains(filterValue, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (!string.IsNullOrEmpty(_sortColumn))
        {
            NjTableColumn<T> column = Configuration.Columns.First(c => c.GetColumnKey() == _sortColumn);
            PropertyInfo propertyInfo = GetPropertyInfo(column.Property);

            if (_sortAscending)
                query = query.OrderBy(item => GetPropertyValue(item, column.Property));
            else
                query = query.OrderByDescending(item => GetPropertyValue(item, column.Property));
        }

        _filteredItems = query.ToList();
        StateHasChanged();
    }

    private IEnumerable<T>
        GetPagedData()
    {
        return _filteredItems
        .Skip((_currentPage - 1) * Configuration.PageSize)
        .Take(Configuration.PageSize);
    }

    private void OnSort(string columnName)
    {
        if (_sortColumn == columnName)
        {
            _sortAscending = !_sortAscending;
        }
        else
        {
            _sortColumn = columnName;
            _sortAscending = true;
        }

        ApplyFiltersAndSort();
    }

    private static object GetPropertyValue(T item, Expression<Func<T, object>> property) => property.Compile()(item);

    private static PropertyInfo GetPropertyInfo(Expression<Func<T, object>> property)
    {
        if (property.Body is UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MemberExpression memberExpression)
                return (PropertyInfo)memberExpression.Member;
        }
        else if (property.Body is MemberExpression memberExpression)
        {
            return (PropertyInfo)memberExpression.Member;
        }

        throw new ArgumentException("Invalid property expression");
    }
}
