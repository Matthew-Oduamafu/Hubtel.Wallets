using Hubtel.Wallets.Application.Models.Identity;
using MediatR;

namespace Hubtel.Wallets.Application.Features.Accounts.Requests
{
    public class LoginCommand : IRequest<IAuthResponse>
    {
        public AuthRequest AuthRequest { get; set; }
    }
}