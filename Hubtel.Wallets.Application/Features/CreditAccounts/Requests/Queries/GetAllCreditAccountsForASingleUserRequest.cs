using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using MediatR;
using System.Collections.Generic;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries
{
    public class GetAllCreditAccountsForASingleUserRequest : IRequest<IReadOnlyList<AllCreditAccountsForUserDto>>
    {
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}