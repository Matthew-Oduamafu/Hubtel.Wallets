using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command
{
    public class UpdateCreditAccountCommand : IRequest<BaseResponse>
    {
        public UpdateCreditAccountDto dto { get; set; }
    }
}