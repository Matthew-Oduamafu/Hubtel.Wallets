using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hubtel.Wallets.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "a9d610be-c847-1cee-cccd-175a022b100d",
                    UserId = "0be64bd1-d201-7821-9000-18937492a66d"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "03871707-51b6-48ca-aeb2-1a6a48c8b25a",
                    UserId = "5ca5f8ba-a92f-e8b2-c666-5667615de41c"
                }
                );
        }
    }
}