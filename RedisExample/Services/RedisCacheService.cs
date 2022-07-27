using StackExchange.Redis;

namespace RedisExample.Services;

public class RedisCacheService: ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    
    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }
    public async Task<string> GetCacheValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task SetCacheValueAsync(string requestKey, string requestValue)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(requestKey, requestValue);
    }
}
