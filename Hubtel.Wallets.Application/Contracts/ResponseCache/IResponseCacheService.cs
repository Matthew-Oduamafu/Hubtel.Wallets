using System;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Contracts.ResponseCache
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan absoluteExpireTime, TimeSpan? unusuedExpireTime = null);

        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}