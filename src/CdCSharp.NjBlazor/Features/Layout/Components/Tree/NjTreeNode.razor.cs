using CdCSharp.NjBlazor.Core.Abstractions.Components;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Tree;

/// <summary>
/// Represents a node in the NjTree structure.
/// </summary>
public partial class NjTreeNode : NjComponentBase
{
    private bool _open = true;

    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the list of child nodes.
    /// </summary>
    /// <value>
    /// The list of child nodes.
    /// </value>
    public List<NjTreeNode> ChildNodes { get; set; } = [];

    /// <summary>
    /// Gets the level.
    /// </summary>
    /// <returns>
    /// The level.
    /// </returns>
    public int Level => GetLevel();

    /// <summary>
    /// Represents the state of being open.
    /// </summary>
    public bool Open { get => _open || (OpenFunction?.Invoke(this) ?? false); set => _open = value; }

    /// <summary>
    /// Gets or sets an optional function to open the node.
    /// </summary>
    /// <value>
    /// The function to open the node.
    /// </value>
    [Parameter]
    public Func<NjTreeNode, bool>? OpenFunction { get; set; }

    /// <summary>
    /// Gets or sets the parent node in a cascading parameter.
    /// </summary>
    /// <value>
    /// The parent node in a cascading parameter.
    /// </value>
    [CascadingParameter]
    public NjTreeNode? ParentNode { get; set; }

    /// <summary>
    /// Gets or sets the parent NjTree in the cascading parameter.
    /// </summary>
    /// <value>
    /// The parent NjTree.
    /// </value>
    [CascadingParameter]
    public NjTree ParentTree { get; set; } = default!;

    /// <summary>
    /// Gets or sets the title to be rendered as a fragment.
    /// </summary>
    /// <value>
    /// The title to be rendered as a fragment.
    /// </value>
    [Parameter]
    public RenderFragment? Title { get; set; }

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
        ChildNodes.Add(node);
        StateHasChanged();
        ParentTree.NotifyChange();
    }

    /// <summary>
    /// This method is called when the parameters are set for the current node. It adds the current
    /// node as a child to the parent tree if the parent node is null, otherwise, it adds the
    /// current node as a child to the parent node.
    /// </summary>
    /// <remarks>
    /// If the parent node is null, the current node is added to the parent tree. If the parent node
    /// is not null, the current node is added to the parent node.
    /// </remarks>
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
    /// Determines whether rendering should occur.
    /// </summary>
    /// <returns>
    /// False, indicating that rendering should not occur.
    /// </returns>
    protected override bool ShouldRender() => false;

    private int GetLevel()
    {
        NjTreeNode? parent = ParentNode;
        int level = 0;
        while (parent != null)
        {
            level++;
            parent = parent.ParentNode;
        }
        return level;
    }
}