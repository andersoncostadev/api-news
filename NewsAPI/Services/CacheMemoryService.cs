using Microsoft.Extensions.Caching.Memory;
using NewsAPI.Services.Interfaces;

namespace NewsAPI.Services;

public class CacheMemoryService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheMemoryService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? Get<T>(string key)
    {
       var cache = _memoryCache.Get<T>(key);
       
       return cache;
    }

    public void Set<T>(string key, T value)
    {
        _memoryCache.Set(key, value);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}