using Microsoft.AspNetCore.Identity;

namespace Hubtel.Wallets.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}