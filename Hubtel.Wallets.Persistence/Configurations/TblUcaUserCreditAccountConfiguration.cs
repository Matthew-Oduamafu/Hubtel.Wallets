using Hubtel.Wallets.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Hubtel.Wallets.Persistence.Configurations
{
    public class TblUcaUserCreditAccountConfiguration : IEntityTypeConfiguration<TblUcaUserCreditAccount>
    {
        public void Configure(EntityTypeBuilder<TblUcaUserCreditAccount> builder)
        {
            builder.HasData(

                new TblUcaUserCreditAccount { UcaIdpk = 1, UcaUserIdfk = "0be64bd1-d201-7821-9000-18937492a66d", UcaTypeIdfk = 1, UcaSchemeIdfk = 3, UcaAccountNumber = "0552235521", UcaCreationDate = DateTime.UtcNow },

                new TblUcaUserCreditAccount { UcaIdpk = 2, UcaUserIdfk = "0be64bd1-d201-7821-9000-18937492a66d", UcaTypeIdfk = 1, UcaSchemeIdfk = 4, UcaAccountNumber = "0552474843", UcaCreationDate = DateTime.UtcNow }
                );
        }
    }
}