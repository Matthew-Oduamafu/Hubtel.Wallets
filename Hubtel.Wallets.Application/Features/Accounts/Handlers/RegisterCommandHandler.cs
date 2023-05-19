using Hubtel.Wallets.Application.Contracts.Identity;
using Hubtel.Wallets.Application.Features.Accounts.Requests;
using Hubtel.Wallets.Application.Models.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Accounts.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegistrationResponse>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.Register(request.RegistrationRequest);
            return response;
        }
    }
}