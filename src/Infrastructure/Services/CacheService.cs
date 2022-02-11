using System;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task CacheAsync(string key, object value, TimeSpan lifetime)
    {
        if (value is null)
        {
            return;
        }

        var serializedResponse = JsonConvert.SerializeObject(key);
        await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = lifetime,
        });
    }

    public async Task<string> GetAsync(string key)
    {
        var cachedResponse = await _distributedCache.GetStringAsync(key);
        return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
    }
}
