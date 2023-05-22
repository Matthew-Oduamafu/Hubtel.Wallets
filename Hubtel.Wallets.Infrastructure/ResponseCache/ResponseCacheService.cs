using Hubtel.Wallets.Application.Contracts.ResponseCache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Infrastructure.ResponseCache
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan absoluteExpireTime, TimeSpan? unusuedExpireTime = null)
        {
            if (response == null) return;

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(
                cacheKey
                , serializedResponse
                , new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpireTime,
                    SlidingExpiration = unusuedExpireTime
                }
                );
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheKey) ? null! : cachedResponse;
        }
    }
}