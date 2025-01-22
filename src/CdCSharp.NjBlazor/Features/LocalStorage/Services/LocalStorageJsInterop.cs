using CdCSharp.NjBlazor.Core.Abstractions.Components;
using CdCSharp.NjBlazor.Features.LocalStorage.Abstractions;
using CdCSharp.NjBlazor.Types;
using Microsoft.JSInterop;
using System.Text.Json;

namespace CdCSharp.NjBlazor.Features.LocalStorage.Services;
public class LocalStorageJsInterop(IJSRuntime jsRuntime)
    : ModuleJsInterop(jsRuntime, CSharpReferences.Modules.LocalStorage), ILocalStorageJsInterop
{
    public async ValueTask SetItemAsync<T>(string key, T value)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        string jsonValue = JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.LocalStorage.ClearAsync, key, jsonValue);
    }

    public async ValueTask<T?> GetItemAsync<T>(string key)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        string jsonValue = await JsRuntime.InvokeAsync<string>(CSharpReferences.Functions.LocalStorage.GetItemAsync, key);

        if (string.IsNullOrEmpty(jsonValue))
            return default;

        return JsonSerializer.Deserialize<T>(jsonValue, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async ValueTask RemoveItemAsync(string key)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.LocalStorage.RemoveItemAsync, key);
    }

    public async ValueTask ClearAsync()
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.LocalStorage.ClearAsync);
    }

    public async ValueTask<string[]> GetKeysByPrefixAsync(string key)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        return await JsRuntime.InvokeAsync<string[]>(CSharpReferences.Functions.LocalStorage.GetKeysByPrefixAsync, key);
    }

    public async ValueTask ClearByPrefixAsync(string prefix)
    {
        await IsModuleTaskLoaded.Task;
        await ModuleTask.Value;
        await JsRuntime.InvokeVoidAsync(CSharpReferences.Functions.LocalStorage.ClearAsync, prefix);
        throw new NotImplementedException();
    }
}