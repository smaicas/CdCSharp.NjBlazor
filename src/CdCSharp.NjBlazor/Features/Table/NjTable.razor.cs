using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace CdCSharp.NjBlazor.Features.Table;
public partial class NjTable<T>
{
    [Parameter]
    public IEnumerable<T> Items { get; set; }

    [Parameter]
    public NjTableConfiguration<T>
        Configuration
    { get; set; }

    private IEnumerable<T> _filteredItems;
    private string _globalFilter = string.Empty;
    private Dictionary<string, string> _columnFilters = [];
    private string _sortColumn;
    private bool _sortAscending = true;
    private int _currentPage = 1;

    protected override void OnInitialized()
    {
        _filteredItems = Items;

        // Inicializar los filtros de columna con claves únicas
        _columnFilters = Configuration.Columns
            .Select((col, index) => new
            {
                Key = GetColumnKey(col, index),
                Value = string.Empty
            })
            .ToDictionary(x => x.Key, x => x.Value);

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
            string columnKey = GetColumnKey(column, i);

            if (_columnFilters.TryGetValue(columnKey, out string? filterValue) &&
                !string.IsNullOrWhiteSpace(filterValue))
            {
                query = query.Where(item =>
                    GetPropertyValue(item, column.Property).ToString()
                        .Contains(filterValue, StringComparison.OrdinalIgnoreCase));
            }
        }

        // ... resto del código de ordenación ...

        _filteredItems = query.ToList();
        StateHasChanged();
    }

    // Método para generar claves únicas para cada columna
    private string GetColumnKey(NjTableColumn<T> column, int columnIndex)
    {
        string propertyName = GetPropertyName(column.Property);
        return $"col_{columnIndex}_{propertyName}";
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

    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        MemberExpression? memberExpression = expression.Body as MemberExpression
            ?? (expression.Body as UnaryExpression)?.Operand as MemberExpression;

        if (memberExpression != null)
            return memberExpression.Member.Name;

        throw new ArgumentException("Invalid property expression");
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
