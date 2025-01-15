using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Core.Cache;
using CdCSharp.NjBlazor.Features.Dom.Abstractions;
using CdCSharp.NjBlazor.Features.ResourceAccess.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;

namespace CdCSharp.NjBlazor.Features.ResourceAccess.Components.FileRenderer;

public partial class NjFileRenderer : NjComponentBase
{
    private bool _showAll;
    private string? _showAllText;
    private RenderFragment? CurrentFragment;
    private List<NjFileRendererResource> files = [];
    private NjFileRendererResource? selectedFile;

    [Parameter]
    public string ContentHeight { get; set; } = "auto";

    [Parameter]
    public List<NjFileRendererResource> FileAssets { get; set; } = [];

    [Parameter]
    public Func<string, RenderFragment>? RenderFragmentFunction { get; set; }

    [Parameter]
    public Func<string, string>? RenderFunction { get; set; }

    [Parameter]
    public string ShowAllText
    {
        get => _showAllText ?? Loc["Show all"];
        set
        {
            if (_showAllText == value) return;
            _showAllText = value;
        }
    }

    private ElementReference _readerReference { get; set; }
    [Inject] private IDOMJsInterop DomJs { get; set; } = default!;
    [Inject] private IEmbeddedResourceAccessor EmbeddedResourceAccessor { get; set; } = default!;
    [Inject] private IStringLocalizer<NjFileRenderer> Loc { get; set; } = default!;
    [Inject] private ICacheService<NjFileRenderer, RenderFragment> RenderFragmentCache { get; set; } = default!;

    protected override void OnInitialized()
    {
        if (FileAssets != null && FileAssets.Count > 0)
        {
            files = FileAssets;
        }
    }

    private void CloseDocument(MouseEventArgs e) => selectedFile = null;

    private async Task FileLoadedAsync(string fileContent) => CurrentFragment = await RenderFileStringAsync(fileContent);

    private async Task GoTopAsync(MouseEventArgs e)
    {
        await DomJs.ScrollTopAsync(_readerReference);
        await DomJs.ScrollToClosestAsync(".nj-file-renderer-content", _readerReference);
    }

    private Task<RenderFragment> RenderFileStringAsync(string content)
    {
        if (RenderFunction != null)
        {
            string renderedContent = RenderFunction(content);
            return Task.FromResult<RenderFragment>(builder => builder.AddMarkupContent(0, renderedContent));
        }
        else if (RenderFragmentFunction != null)
        {
            return Task.FromResult<RenderFragment>(RenderFragmentFunction(content));
        }

        return Task.FromResult<RenderFragment>(builder => builder.AddContent(0, string.Empty));
    }

    private Task ShowAllTriggerAsync()
    {
        _showAll = !_showAll;
        return Task.CompletedTask;
    }

    private async Task ShowFileAsync(NjFileRendererResource file)
    {
        selectedFile = file;
        if (RenderFragmentCache.TryGet(file.ResourcePath, out RenderFragment? cachedFragment) && cachedFragment != null)
        {
            CurrentFragment = cachedFragment;
        }
        else
        {
            string content = await EmbeddedResourceAccessor.GetResourceContentAsync(selectedFile.ResourcePath);
            CurrentFragment = await RenderFileStringAsync(content);
            RenderFragmentCache.Set(selectedFile.ResourcePath, CurrentFragment);
        }
    }
}