using Hubtel.Wallets.Application.Contracts.Identity;
using Hubtel.Wallets.Application.Features.Accounts.Requests;
using Hubtel.Wallets.Application.Models.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Accounts.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.Login(request.AuthRequest);
            return response;
        }
    }
}