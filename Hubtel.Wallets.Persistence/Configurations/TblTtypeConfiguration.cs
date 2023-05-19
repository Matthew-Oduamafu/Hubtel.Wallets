using Hubtel.Wallets.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Hubtel.Wallets.Persistence.Configurations
{
    public class TblTtypeConfiguration : IEntityTypeConfiguration<TblTtype>
    {
        public void Configure(EntityTypeBuilder<TblTtype> builder)
        {
            builder.HasData(
                new TblTtype { TIdpk = 1, TTypeName = "Momo", CreationDate = DateTime.Now },
                new TblTtype { TIdpk = 2, TTypeName = "Card", CreationDate = DateTime.Now }
                );
        }
    }
}