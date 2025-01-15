using System.Linq.Expressions;

namespace CdCSharp.NjBlazor.Features.Table;

public class NjTableColumn<T>
{
    public INjCellComponent CustomComponent { get; set; }
    public bool Filterable { get; set; } = true;
    public string FilterPlaceholder { get; set; }
    public Expression<Func<T, object>> Property { get; set; }
    public bool Sortable { get; set; } = true;
    public string Title { get; set; }
    public string Width { get; set; }

    public string GetColumnKey()
    {
        string propertyName = GetPropertyName(Property);
        string typeName = typeof(T).Name;
        return $"col_{typeof(T).Name}_{propertyName}";
    }

    public static string GetPropertyName<TItem, TProperty>(Expression<Func<TItem, TProperty>> expression)
    {
        // Handle MemberExpression (e.g., p => p.Id)
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        // Handle UnaryExpression that wraps a MemberExpression
        if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
        {
            return unaryMemberExpression.Member.Name;
        }

        throw new ArgumentException("Expression must refer to a property or a string constant.");
    }
}