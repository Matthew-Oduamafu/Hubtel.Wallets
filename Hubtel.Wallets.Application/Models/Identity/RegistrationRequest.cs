using System.ComponentModel.DataAnnotations;

namespace Hubtel.Wallets.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public string UserName
        {
            get
            {
                return LastName.ToUpper() + "" + FirstName;
            }
        }

        [Required]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}