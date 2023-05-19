using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hubtel.Wallets.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = "a9d610be-c847-1cee-cccd-175a022b100d", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "03871707-51b6-48ca-aeb2-1a6a48c8b25a", Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
                );
        }
    }
}