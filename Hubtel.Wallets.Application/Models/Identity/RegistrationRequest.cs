namespace Hubtel.Wallets.Application.Models.Identity
{
    public class RegistrationRequest : AuthRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName
        {
            get
            {
                return LastName.ToUpper() + "" + FirstName;
            }
        }

        public string PhoneNumber { get; set; }
    }
}