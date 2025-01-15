using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Media.Components;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Tree;

/// <summary>
/// Represents a tree component that inherits from the base component class.
/// </summary>
public partial class NjTree : NjComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered as a child component.
    /// </summary>
    /// <value>
    /// The content to be rendered as a child component.
    /// </value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the deep padding value.
    /// </summary>
    /// <value>
    /// The deep padding value.
    /// </value>
    [Parameter]
    public double DeepPadding { get; set; } = 0.5;

    /// <summary>
    /// Gets or sets the flex direction mode for the component.
    /// </summary>
    /// <value>
    /// The flex direction mode to set for the component.
    /// </value>
    [Parameter]
    public FlexDirectionMode Direction { get; set; }

    private List<NjTreeNode> ChildNodes { get; set; } = [];

    /// <summary>
    /// Adds a child node to the current node.
    /// </summary>
    /// <param name="node">
    /// The node to be added as a child.
    /// </param>
    /// <remarks>
    /// If the node is already a child of the current node, it will not be added again.
    /// </remarks>
    public void AddChildNode(NjTreeNode node)
    {
        if (ChildNodes.Contains(node))
            return;
        ChildNodes.Add(node);
        StateHasChanged();
    }

    /// <summary>
    /// Notifies the component that a change has occurred and triggers a re-render.
    /// </summary>
    public void NotifyChange() => StateHasChanged();

    /// <summary>
    /// Method called after rendering the component.
    /// </summary>
    /// <param name="firstRender">
    /// A boolean value indicating if it is the first render of the component.
    /// </param>
    /// <remarks>
    /// If it is the first render, this method iterates through the child nodes, closes them
    /// recursively, and updates the state.
    /// </remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender)
        {
            foreach (NjTreeNode node in ChildNodes)
            {
                CloseNodesRecursive(node);
                StateHasChanged();
            }
        }
    }

    private void CloseNodesRecursive(NjTreeNode node)
    {
        node.Open = false;
        foreach (NjTreeNode subNode in node.ChildNodes)
        {
            CloseNodesRecursive(subNode);
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
                    $"padding-left:{(childNode.Level * DeepPadding).ToString(CultureInfo.InvariantCulture)}rem;"
                );

                if (childNode.ChildNodes.Any())
                {
                    builder.OpenComponent<NjSvgIcon>(7);

                    if (childNode.Open)
                    {
                        builder.AddAttribute(
                            8,
                            "Icon",
                            CdCSharp.NjBlazor.Features.Media.Icons.NjIcons.Materials.MaterialIcons.i_arrow_drop_down
                        );
                    }
                    else
                    {
                        builder.AddAttribute(
                            8,
                            "Icon",
                            CdCSharp.NjBlazor.Features.Media.Icons.NjIcons.Materials.MaterialIcons.i_arrow_right
                        );
                    }
                    builder.CloseComponent();
                }

                builder.AddContent(9, childNode.Title);
                builder.CloseElement();

                if (childNode.Open)
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

    private void ToggleNode(NjTreeNode node) => node.Open = !node.Open;
}