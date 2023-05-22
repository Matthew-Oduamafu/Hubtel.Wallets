using Hubtel.Wallets.Application.Contracts.ResponseCache;
using Hubtel.Wallets.Infrastructure.ResponseCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Api.Middleware.CustomHttpAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _absoluteExpreTime;
        private readonly int _unusedExpireTime;

        public CachedAttribute(int absoluteExpireTime, int unusedExpireTime)
        {
            _absoluteExpreTime = absoluteExpireTime;

            _unusedExpireTime = unusedExpireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // before
            // check if there is cache and return it

            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult; // cached data

                return;
            }

            var executedContext = await next();
            // if no cache continue and save response as cache
            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                if (_unusedExpireTime == 0)
                {
                    await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_absoluteExpreTime), null);
                }
                else if (_unusedExpireTime > 0)
                {
                    await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_absoluteExpreTime), TimeSpan.FromSeconds(_unusedExpireTime));
                }
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder().Append(request.Path);
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append('|').Append(key).Append('-').Append(value);
            }

            return keyBuilder.ToString();
        }
    }
}