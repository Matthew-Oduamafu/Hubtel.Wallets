using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Persistence.Context;
using Hubtel.Wallets.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hubtel.Wallets.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<HubtelWalletsDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("HubtelWalletsConnString")!
                , sqlServerOptionsAction:
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
            ));

            // add repositories and unitOfWork
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}