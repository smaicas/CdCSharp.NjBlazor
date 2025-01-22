using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Cache;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Media.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Tree;

/// <summary>
/// Componente principal del árbol (NjTree) que gestiona los nodos y su estado.
/// </summary>
public partial class NjTree : NjComponentBase
{
    [Inject]
    public ICacheService<NjTree, bool> CacheService { get; set; } = default!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public int DeepPadding { get; set; } = 1;

    [Parameter]
    public FlexDirectionMode Direction { get; set; }

    [Parameter]
    public bool HasMemory { get; set; } = true; // Habilita el estado persistente.

    private List<NjTreeNode> ChildNodes { get; set; } = [];
    public int CurrentCount => ChildNodes.Count;
    protected override string? PersistentId => $"njtree";

    /// <summary>
    /// Añade un nodo hijo al árbol si no está ya incluido.
    /// </summary>
    /// <param name="node">Nodo a agregar.</param>
    public void AddChildNode(NjTreeNode node)
    {
        if (!ChildNodes.Contains(node))
        {

            node.NodePosition = $"{CurrentCount}";

            ChildNodes.Add(node);
            StateHasChanged(); // Actualiza el árbol al agregar un nuevo nodo.
        }
    }

    /// <summary>
    /// Notifica al árbol que su estado ha cambiado.
    /// </summary>
    public void NotifyChange() => StateHasChanged();

    /// <summary>
    /// Restaura el estado del árbol tras el primer render.
    /// </summary>
    /// <param name="firstRender">Indica si es la primera vez que se renderiza.</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender && HasMemory)
        {
            await RestoreTreeState();
            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// Restaura el estado de todos los nodos hijos.
    /// </summary>
    private async Task RestoreTreeState()
    {
        foreach (NjTreeNode node in ChildNodes)
        {
            await RestoreNodeState(node);
        }
    }

    /// <summary>
    /// Restaura el estado de un nodo específico y sus hijos recursivamente.
    /// </summary>
    /// <param name="node">Nodo cuyo estado se debe restaurar.</param>
    private async Task RestoreNodeState(NjTreeNode node)
    {
        (bool Success, bool Value) = await CacheService.TryGetAsync(GetNodeStateKey(node));
        if (Success)
        {
            node.CachedOpen = Value;
        }
        foreach (NjTreeNode childNode in node.ChildNodes)
        {
            await RestoreNodeState(childNode);
        }
    }

    private RenderFragment RenderChildNodes(NjTreeNode node)
    {
        return builder =>
        {
            foreach (NjTreeNode childNode in node.ChildNodes)
            {
                builder.OpenElement(1, "div");
                builder.AddAttribute(2, "class", "nj-tree-node");
                builder.OpenElement(3, "div");
                builder.AddAttribute(4, "class", "nj-tree-node-title");
                builder.AddAttribute(
                    5,
                    "onclick",
                    EventCallback.Factory.Create(this, () => ToggleNode(childNode))
                );
                builder.AddAttribute(
                    6,
                    "style",
                    $"padding-left:{CssTools.CalculateCssTreePaddingValue(childNode.DeepLevel * DeepPadding)}"
                );
                if (childNode.ChildNodes.Any())
                {
                    builder.OpenComponent<NjSvgIcon>(7);
                    if (childNode.CachedOpen)
                    {
                        builder.AddAttribute(
                            8,
                            "Icon",
                            Media.Icons.NjIcons.Materials.MaterialIcons.i_arrow_drop_down
                        );
                    }
                    else
                    {
                        builder.AddAttribute(
                            8,
                            "Icon",
                            Media.Icons.NjIcons.Materials.MaterialIcons.i_arrow_right
                        );
                    }
                    builder.CloseComponent();
                }
                builder.AddContent(9, childNode.Title);
                builder.CloseElement();
                if (childNode.CachedOpen)
                {
                    builder.OpenElement(10, "div");
                    builder.AddAttribute(11, "class", "nj-tree-node-childs");
                    builder.AddContent(12, RenderChildNodes(childNode));
                    builder.CloseElement();
                }
                builder.CloseElement();
            }
        };
    }

    /// <summary>
    /// Genera una clave única para almacenar el estado del nodo.
    /// </summary>
    /// <param name="node">Nodo para el cual generar la clave.</param>
    /// <returns>Clave única basada en el árbol y el nodo.</returns>
    private string GetNodeStateKey(NjTreeNode node) =>
        $"NjTree:{UniqueId}:Node:{node.UniqueId}";

    private void ToggleNode(NjTreeNode node) => node.ToggleOpenState();
}
