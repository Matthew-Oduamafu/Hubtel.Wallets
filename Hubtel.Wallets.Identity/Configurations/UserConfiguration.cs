using Hubtel.Wallets.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hubtel.Wallets.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "0be64bd1-d201-7821-9000-18937492a66d",
                    FirstName = "Matthew",
                    LastName = "Oduamafu",
                    UserName = "ODUAMAFU, Matthew",
                    NormalizedUserName = "ODUAMAFU, MATTHEW",
                    Email = "mattoduamafu@gmail.com",
                    NormalizedEmail = "MATTODUAMAFU@GMAIL.COM",
                    PhoneNumber = "0552474843",
                    PasswordHash = hasher.HashPassword(null, "Password@1"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "5ca5f8ba-a92f-e8b2-c666-5667615de41c",
                    FirstName = "System",
                    LastName = "Hubtel GH",
                    UserName = "HUBTEL GH, system",
                    NormalizedUserName = "HUBTEL GH, SYSTEM",
                    Email = "hubtel.info.gh@gmail.com",
                    NormalizedEmail = "HUBTEL.INFO.GH@GMAIL.COM",
                    PhoneNumber = "0552235521",
                    PasswordHash = hasher.HashPassword(null, "Password@2"),
                    EmailConfirmed = true
                }
                );
        }
    }
}