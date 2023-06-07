using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Hubtel.Wallets.Api.Middleware;
using Hubtel.Wallets.Application;
using Hubtel.Wallets.Identity;
using Hubtel.Wallets.Infrastructure;
using Hubtel.Wallets.Persistence;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Hubtel.Wallets.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //configure api route to lowercase
            services.Configure<RouteOptions>(opts =>
            {
                opts.LowercaseQueryStrings = true;
                opts.LowercaseUrls = true;
            });

            // configure Redis caching
            services.ConfigureRedisCacheService(this.Configuration);

            services.AddMemoryCache();  // set up memory cache for rate limiting
            services.ConfigureRateLimiting();

            services.ConfigureInfrastructureService(this.Configuration);
            services.ConfigureApplicationService();

            // Add services to the container.
            services.AddControllers();

            services.ConfigureVersioning();  // setting up API versioning

            AddSwaggerDoc(services);

            services.ConfigurePersistenceService(this.Configuration);
            services.ConfigureIdentityService(this.Configuration);
        }

        private void AddSwaggerDoc(IServiceCollection services)
        {
            // Add FV to Asp.net
            services.AddFluentValidationClientsideAdapters();

            // Add FV
            //services.AddFluentValidationAutoValidation()
            //.AddFluentValidationClientsideAdapters();

            // Adds FluentValidationRules staff to Swagger.
            services.AddFluentValidationRulesToSwagger();

            // Adds logging
            services.AddLogging(builder => builder.AddConsole());

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] an then your token in the next input below.\r\n\r\nExample: 'Bearer 1234etetrf'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme ="oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hubtel.Wallet.Api",
                    Version = "v1",
                    Description = "An API service to be used to manage a user's wallet on the Hubtel app",
                    Contact = new OpenApiContact
                    {
                        Email = "mattoduamafu@gmail.com",
                        Extensions = { },
                        Name = "Matthew",
                        Url = new Uri("https://github.com/Matthew-Oduamafu")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://github.com/")
                    },
                    Extensions = { },
                    TermsOfService = new Uri("https://github.com/Matthew-Oduamafu")
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            // configure custom error handler
            app.ConfigureExceptionHabdler();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hubtel.Wallets.Api V1"));

            app.UseHttpsRedirection();

            app.UseIpRateLimiting();  // register rate limiting in middle ware
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}