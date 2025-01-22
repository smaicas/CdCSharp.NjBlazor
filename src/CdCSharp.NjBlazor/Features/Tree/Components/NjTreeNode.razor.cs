using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Tree;

/// <summary>
/// Nodo individual de un árbol (NjTreeNode) que puede contener hijos.
/// </summary>
public partial class NjTreeNode : NjComponentBase
{
    private bool _open = false; // Estado abierto/cerrado del nodo.

    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public List<NjTreeNode> ChildNodes { get; set; } = [];

    [CascadingParameter]
    public NjTree ParentTree { get; set; } = default!;

    [Parameter]
    public RenderFragment? Title { get; set; }

    public bool CachedOpen { get; set; }

    protected override string? PersistentId => $"{NodePosition}";

    /// <summary>
    /// Propiedad Open (estado del nodo) con soporte para persistencia.
    /// </summary>
    public async Task<bool> GetOpenAsync()
    {
        if (ParentTree.HasMemory)
        {
            (bool success, bool cachedOpen) = await ParentTree.CacheService.TryGetAsync(GetNodeStateKey());
            if (success)
            {
                _open = cachedOpen;
            }
        }
        return _open;
    }

    public async Task SetOpenAsync(bool value)
    {
        _open = value;
        if (ParentTree.HasMemory)
        {
            await ParentTree.CacheService.SetAsync(GetNodeStateKey(), value);
        }
    }

    public async Task InitializeOpenStateAsync()
    {
        if (ParentTree.HasMemory)
        {
            (bool success, bool cachedOpen) = await ParentTree.CacheService.TryGetAsync(GetNodeStateKey());
            CachedOpen = success ? cachedOpen : _open;
        }
        else
        {
            CachedOpen = _open;
        }
    }

    public void ToggleOpenState()
    {
        _open = !CachedOpen;
        CachedOpen = _open;

        if (ParentTree.HasMemory)
        {
            _ = ParentTree.CacheService.SetAsync(GetNodeStateKey(), _open);
        }
    }

    /// <summary>
    /// Adds a child node to the current node.
    /// </summary>
    /// <param name="node">
    /// The node to be added as a child.
    /// </param>
    /// <remarks>
    /// If the node is already a child of the current node, it will not be added again. After adding
    /// the node, the state of the node is updated, and the parent tree is notified of the change.
    /// </remarks>
    public void AddChildNode(NjTreeNode node)
    {
        if (ChildNodes.Contains(node))
            return;

        if (ParentNode == null)
        {
            node.NodePosition = $"{ParentTree.CurrentCount}";
        }
        else
        {
            node.NodePosition = $"{ParentNode.NodePosition}.{ParentTree.CurrentCount}";
        }

        ChildNodes.Add(node);
        StateHasChanged();
        ParentTree.NotifyChange();
    }

    private int? _deepLevel; // Nivel de profundidad calculado y almacenado en caché.

    /// <summary>
    /// Devuelve el nivel de profundidad del nodo en el árbol.
    /// </summary>
    public int DeepLevel => _deepLevel ??= CalculateLevel();
    public int CurrentCount => ChildNodes.Count;

    public string? NodePosition { get; set; }

    [CascadingParameter]
    public NjTreeNode? ParentNode { get; set; }
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (ParentNode == null)
        {

            ParentTree.AddChildNode(this);
        }
        else
        {
            ParentNode.AddChildNode(this);
        }
    }

    /// <summary>
    /// Genera una clave única para identificar el nodo en el almacenamiento.
    /// </summary>
    /// <returns>Clave única basada en el árbol y propiedades del nodo.</returns>
    private string GetNodeStateKey() =>
        $"NjTree:{ParentTree.UniqueId}:Node:{UniqueId}";

    /// <summary>
    /// Calcula el nivel del nodo basado en sus ancestros.
    /// </summary>
    /// <returns>Nivel de profundidad del nodo.</returns>
    private int CalculateLevel()
    {
        int level = 0;
        NjTreeNode? parent = ParentNode;
        while (parent != null)
        {
            level++;
            parent = parent.ParentNode;
        }
        return level;
    }
}
