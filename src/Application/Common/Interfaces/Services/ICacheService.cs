using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services;

public interface ICacheService
{
    Task CacheAsync(string Key, object value, TimeSpan lifetime);
    Task<string> GetAsync(string Key);
}
