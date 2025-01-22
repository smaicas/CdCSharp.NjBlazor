namespace CdCSharp.NjBlazor.Features.LocalStorage.Abstractions;
public interface ILocalStorageJsInterop
{
    ValueTask SetItemAsync<T>(string key, T value);
    ValueTask<T?> GetItemAsync<T>(string key);
    ValueTask<string[]> GetKeysByPrefixAsync(string key);
    ValueTask RemoveItemAsync(string key);
    ValueTask ClearAsync();
    ValueTask ClearByPrefixAsync(string prefix);
}
