using Microsoft.AspNetCore.Mvc;
using RedisExample.Models;
using RedisExample.Services;

namespace RedisExample.Controllers;

[ApiController]
public class CacheController : Controller
{
    private readonly ICacheService _cacheService;

    public CacheController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("cache/{key}")]
    public async Task<IActionResult> GetCacheValue([FromRoute] string key)
    {
        var value = await _cacheService.GetCacheValueAsync(key);
        return string.IsNullOrEmpty(value) ? NotFound() : Ok(value);
    }
    
    [HttpPost("cache")]
    public async Task<IActionResult> GetCacheValue([FromBody] NewCacheEntryRequest request)
    {
        await _cacheService.SetCacheValueAsync(request.Key, request.Value);
        return Ok();
    }
}
