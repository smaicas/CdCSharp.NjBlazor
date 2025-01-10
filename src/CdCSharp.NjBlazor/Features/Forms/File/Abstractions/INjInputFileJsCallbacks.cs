namespace CdCSharp.NjBlazor.Features.Forms.File.Abstractions;

/// <summary>
/// Interface to implement callbacks for <see cref="NjInputFileJsCallbacksRelay" />
/// </summary>
internal interface INjInputFileJsCallbacks
{
    Task NotifyChangeAsync(NjBrowserFile[] files);
}