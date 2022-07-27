using Microsoft.Extensions.Caching.Memory;

namespace RedisExample.Services;

public class InMemoryCacheService : ICacheService
{
    private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    public Task<string> GetCacheValueAsync(string key)
    {
        return Task.FromResult(_cache.Get<string>(key));
    }

    public Task SetCacheValueAsync(string requestKey, string requestValue)
    {
        _cache.Set(requestKey, requestValue);
        return Task.CompletedTask;
    }
}
