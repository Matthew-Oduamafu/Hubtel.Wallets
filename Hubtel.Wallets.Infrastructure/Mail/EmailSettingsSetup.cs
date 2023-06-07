using Hubtel.Wallets.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Hubtel.Wallets.Infrastructure.Mail
{
    public class EmailSettingsSetup : IConfigureOptions<EmailSettings>
    {
        private readonly IConfiguration _configuration;

        public EmailSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EmailSettings options)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey", EnvironmentVariableTarget.User);
            options.ApiKey = apiKey;

            _configuration.GetSection("EmailSettings").Bind(options);
        }
    }
}