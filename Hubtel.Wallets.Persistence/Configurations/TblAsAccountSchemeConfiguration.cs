using Hubtel.Wallets.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Hubtel.Wallets.Persistence.Configurations
{
    public class TblAsAccountSchemeConfiguration : IEntityTypeConfiguration<TblAsAccountScheme>
    {
        public void Configure(EntityTypeBuilder<TblAsAccountScheme> builder)
        {
            builder.HasData(
                new TblAsAccountScheme { AsIdpk = 1, AsTypeIdfk = 2, AsSchemeName = "Mastercard", CreationDate = DateTime.UtcNow },
                new TblAsAccountScheme { AsIdpk = 2, AsTypeIdfk = 2, AsSchemeName = "Visa", CreationDate = DateTime.UtcNow },
                new TblAsAccountScheme { AsIdpk = 3, AsTypeIdfk = 1, AsSchemeName = "MTN", CreationDate = DateTime.UtcNow },
                new TblAsAccountScheme { AsIdpk = 4, AsTypeIdfk = 1, AsSchemeName = "Vodafone", CreationDate = DateTime.UtcNow },
                new TblAsAccountScheme { AsIdpk = 5, AsTypeIdfk = 1, AsSchemeName = "AirtelTigo", CreationDate = DateTime.UtcNow }
                );
        }
    }
}