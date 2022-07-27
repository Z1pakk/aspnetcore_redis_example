namespace RedisExample.Services;

public interface ICacheService
{
    Task<string> GetCacheValueAsync(string key);
    Task SetCacheValueAsync(string requestKey, string requestValue);
}
    
