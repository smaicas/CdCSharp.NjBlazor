using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace CdCSharp.NjBlazor.Features.Table;
public interface INjCellComponent
{
    RenderFragment CreateComponent<TItem>(TItem item, PropertyInfo property);
}
