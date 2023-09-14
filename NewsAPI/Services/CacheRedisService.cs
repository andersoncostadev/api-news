using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NewsAPI.Services.Interfaces;

namespace NewsAPI.Services;

public class CacheRedisService : ICacheService
{
    private readonly IDistributedCache _redisCache;
    private readonly DistributedCacheEntryOptions _options;
    
    public CacheRedisService(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120)
        };
    }
    public T? Get<T>(string key)
    {
        var cache = _redisCache.Get(key);

        if (cache is null)
            return default;

        var result = JsonSerializer.Deserialize<T>(cache);
       
        return result;
    }

    public void Set<T>(string key, T value)
    {
        var contentAsString = JsonSerializer.Serialize(value);
        _redisCache.SetString(key, contentAsString, _options);
    }

    public void Remove(string key)
    {
        _redisCache.Remove(key);
    }
}