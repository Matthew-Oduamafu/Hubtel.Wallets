using Hubtel.Wallets.Application.Models.Identity;
using MediatR;

namespace Hubtel.Wallets.Application.Features.Accounts.Requests
{
    public class RegisterCommand : IRequest<RegistrationResponse>
    {
        public RegistrationRequest RegistrationRequest { get; set; }
    }
}