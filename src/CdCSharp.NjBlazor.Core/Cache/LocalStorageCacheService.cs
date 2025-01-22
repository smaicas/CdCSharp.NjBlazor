using Microsoft.JSInterop;
using System.Text.Json;

namespace CdCSharp.NjBlazor.Core.Cache;
/// <summary>
/// Hybrid cache service that combines in-memory storage with localStorage persistence.
/// </summary>
public class LocalStorageCacheService<TRoot, TValue> : InMemoryCacheService<TRoot, TValue>
{
    private readonly IJSRuntime _jsRuntime;

    /// <summary>
    /// Constructor that injects IJSRuntime for localStorage interaction.
    /// </summary>
    /// <param name="jsRuntime">Interface to execute JavaScript in Blazor.</param>
    public LocalStorageCacheService(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

    public override async Task SetAsync(string key, TValue value)
    {
        await base.SetAsync(key, value);
        string jsonValue = JsonSerializer.Serialize(value);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, jsonValue);
    }

    public override async Task<(bool Success, TValue? Value)> TryGetAsync(string key)
    {
        (bool success, TValue value) = await base.TryGetAsync(key);
        if (success)
        {
            return (true, value);
        }

        string localStorageValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        if (localStorageValue != null)
        {
            value = JsonSerializer.Deserialize<TValue>(localStorageValue);
            if (value != null)
            {
                await base.SetAsync(key, value);
                return (true, value);
            }
        }

        return (false, default);
    }
}