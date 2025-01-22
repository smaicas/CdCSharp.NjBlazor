using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.Audio.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CdCSharp.NjBlazor.Features.Audio.Services;

public class AudioJsInterop(IJSRuntime jsRuntime) : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.AudioJs), IAudioJsInterop
{
    public async ValueTask<string> SetAudioSourceAsync(ElementReference elRef)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.SetAudioSource, elRef);
    }

    public async ValueTask StartRecordingAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.StartRecording);
    }

    public async ValueTask<object> StopRecordingAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<object>(CSharpReferences.Functions.StopRecording);
    }

    public async ValueTask<string> VisualizeCanvasAsync(ElementReference canvasElementReference)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.VisualizeCanvas, canvasElementReference);
    }
}