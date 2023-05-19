using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using MediatR;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries
{
    public class GetSingleCreditAccountsForAUserRequest : IRequest<AllCreditAccountsForUserDto>
    {
        public int Id { get; set; }
    }
}