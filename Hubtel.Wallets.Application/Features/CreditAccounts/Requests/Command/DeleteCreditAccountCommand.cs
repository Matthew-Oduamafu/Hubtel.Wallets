using Hubtel.Wallets.Application.Models;
using MediatR;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command
{
    public class DeleteCreditAccountCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}