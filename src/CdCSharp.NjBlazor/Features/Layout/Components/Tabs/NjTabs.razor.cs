using CdCSharp.NjBlazor.Core;
using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Css;
using CdCSharp.NjBlazor.Features.Controls.Components.Button.TextButton.ActivableTextButton;
using Microsoft.AspNetCore.Components;

namespace CdCSharp.NjBlazor.Features.Layout.Components.Tabs;

/// <summary>
/// Represents a custom tab control component.
/// </summary>
/// <remarks>
/// This class extends the functionality of the base component <see cref="NjComponentBase"/>.
/// </remarks>
public partial class NjTabs : NjComponentBase
{
    /// <summary>Gets or sets the content to be rendered as a child component.</summary>
    /// <value>The content to be rendered as a child component.</value>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private RenderFragment? CurrentContent { get; set; }

    /// <summary>Gets or sets the position of the header in the NjTabs component.</summary>
    /// <value>The position of the header in the NjTabs component.</value>
    [Parameter]
    public NjTabsHeaderPosition HeaderPosition { get; set; } = NjTabsHeaderPosition.Top;

    private readonly Dictionary<NjTabSection, RenderFragment> renderedContent = [];
    private string HeaderPositionClass =>
        HeaderPosition switch
        {
            NjTabsHeaderPosition.Right => CssClassReferences.Tabs.TabsRowReverse,
            NjTabsHeaderPosition.Left => CssClassReferences.Tabs.TabsRow,
            NjTabsHeaderPosition.Bottom => CssClassReferences.Tabs.TabsColumnReverse,
            _ => CssClassReferences.Tabs.TabsColumn,
        };

    /// <summary>Gets or sets the padding value for the content.</summary>
    /// <value>The padding value for the content.</value>
    [Parameter]
    public int ContentPadding { get; set; }

    private NjActivableTextButtonVariant ActivableButtonVariant =
        NjActivableTextButtonVariant.UnderLine;

    private string ContentPaddingClass => CssTools.CalculateCssPaddingClass(ContentPadding);

    private List<NjTabSection> TabSections { get; set; } = [];
    private NjTabSection? _activeSection;

    private string _displacementClass = "from-left";

    /// <summary>Set the variant of an activable button based on the header position.</summary>
    /// <remarks>
    /// The variant of the activable button is determined by the header position:
    /// - If the header is on the right, the variant is set to LeftLine.
    /// - If the header is on the left, the variant is set to RightLine.
    /// - If the header is at the bottom, the variant is set to TopLine.
    /// - If the header is at the top or an unknown position, the variant is set to UnderLine.
    /// </remarks>
    protected override void OnParametersSet()
    {
        ActivableButtonVariant = HeaderPosition switch
        {
            NjTabsHeaderPosition.Right => NjActivableTextButtonVariant.LeftLine,
            NjTabsHeaderPosition.Left => NjActivableTextButtonVariant.RightLine,
            NjTabsHeaderPosition.Bottom => NjActivableTextButtonVariant.TopLine,
            NjTabsHeaderPosition.Top or _ => NjActivableTextButtonVariant.UnderLine
        };
    }

    /// <summary>
    /// Method called after the component has been rendered.
    /// </summary>
    /// <param name="firstRender">A boolean value indicating if this is the first render of the component.</param>
    /// <remarks>
    /// If it is the first render, sets the first tab section as active, assigns the first tab section as the active section if not already set,
    /// and updates the current content to be displayed. Finally, triggers a re-render of the component.
    /// </remarks>
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            if (TabSections.Any())
            {
                TabSections[0].Active = true;
                _activeSection ??= TabSections[0];
                CurrentContent = _activeSection.ChildContent;
            }

            StateHasChanged();
        }
    }

    /// <summary>Adds a tab section to the list of tab sections if it is not already present.</summary>
    /// <param name="tabSection">The tab section to add.</param>
    public void AddSection(NjTabSection tabSection)
    {
        if (TabSections.Contains(tabSection))
            return;

        TabSections.Add(tabSection);
    }

    private Task SetActiveSectionAsync(NjTabSection section)
    {
        if (_activeSection == section)
            return Task.CompletedTask;

        if (_activeSection != null)
        {
            _activeSection.Active = false;
            section.Active = true;
            int currentIndex = TabSections.IndexOf(_activeSection);
            int nextIndex = TabSections.IndexOf(section);
            if (nextIndex > currentIndex)
            {
                _displacementClass = "from-left";
            }
            else
            {
                _displacementClass = "from-right";
            }
        }

        if (renderedContent.TryGetValue(section, out RenderFragment? cachedContent))
        {
            CurrentContent = cachedContent;
        }
        else
        {
            RenderContentForSection(section);
        }
        _activeSection = section;

        return Task.CompletedTask;
    }

    private void RenderContentForSection(NjTabSection section)
    {
        renderedContent[section] = section.ChildContent;
        CurrentContent = section.ChildContent;
    }
}
