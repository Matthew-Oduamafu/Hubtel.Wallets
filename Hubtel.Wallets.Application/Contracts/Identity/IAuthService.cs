using Hubtel.Wallets.Application.Models.Identity;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);

        Task<bool> UserExistsByIdAsync(string userId);
    }
}