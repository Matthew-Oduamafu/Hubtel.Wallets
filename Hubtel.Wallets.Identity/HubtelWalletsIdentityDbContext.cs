using Hubtel.Wallets.Identity.Configurations;
using Hubtel.Wallets.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.Wallets.Identity
{
    public class HubtelWalletsIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public HubtelWalletsIdentityDbContext(DbContextOptions<HubtelWalletsIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}