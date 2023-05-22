using Hubtel.Wallets.Application.Contracts.Infrastructure.Mail;
using Hubtel.Wallets.Application.Contracts.ResponseCache;
using Hubtel.Wallets.Application.Models;
using Hubtel.Wallets.Infrastructure.Mail;
using Hubtel.Wallets.Infrastructure.ResponseCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hubtel.Wallets.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }

        public static void ConfigureRedisCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);

            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled) return;

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisCacheSettings.RedisConnectionString;
                options.InstanceName = "Hubtel.Wallet.Api_";
            });

            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}