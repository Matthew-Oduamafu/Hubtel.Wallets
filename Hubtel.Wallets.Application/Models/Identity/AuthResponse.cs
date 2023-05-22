namespace Hubtel.Wallets.Application.Models.Identity
{
    public class AuthResponse : IAuthResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}