using System.Collections.Concurrent;

namespace CdCSharp.NjBlazor.Core.Cache;

/// <summary>
/// In-memory implementation of the cache service.
/// </summary>
public class InMemoryCacheService<TRoot, TValue> : ICacheService<TRoot, TValue>
{
    private readonly ConcurrentDictionary<string, TValue> _cache = new();

    public virtual async Task SetAsync(string key, TValue value)
    {
        _cache[key] = value;
        await Task.CompletedTask;
    }

    public virtual async Task<(bool Success, TValue? Value)> TryGetAsync(string key)
    {
        bool success = _cache.TryGetValue(key, out TValue? value);
        return await Task.FromResult((success, value));
    }
}

/// <summary>
/// In-memory implementation of the cache service with object type.
/// </summary>
public class InMemoryCacheService<TRoot> : InMemoryCacheService<TRoot, object>
{
}

/// <summary>
/// Default in-memory implementation of the cache service.
/// </summary>
public class InMemoryCacheService : InMemoryCacheService<DefaultCacheRoot, object>
{
}
