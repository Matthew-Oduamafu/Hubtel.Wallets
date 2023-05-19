using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command
{
    public class CreateCreditAccountCommand : IRequest<BaseResponse>
    {
        public CreateCreditAccountDto dto { get; set; }
    }
}