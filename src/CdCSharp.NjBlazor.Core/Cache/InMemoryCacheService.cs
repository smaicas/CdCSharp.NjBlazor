namespace CdCSharp.NjBlazor.Core.Cache;

public class InMemoryCacheService<TRoot, TValue> : ICacheService<TRoot, TValue>
{
    private readonly Dictionary<string, TValue> _cache = [];

    public void Set(string key, TValue value) => _cache[key] = value;

    public bool TryGet(string key, out TValue? value) => _cache.TryGetValue(key, out value);
}

public class InMemoryCacheService<TRoot> : InMemoryCacheService<TRoot, object>
{ }

public class InMemoryCacheService : InMemoryCacheService<DefaultCacheRoot, object>
{ }