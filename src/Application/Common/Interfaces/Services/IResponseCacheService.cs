using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services;

public interface IResponseCacheService
{
    Task CacheAsync(string cacheKey, object response, TimeSpan lifetime);
    Task<string> GetCacheAsync(string cacheKey);
}
