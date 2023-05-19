using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using MediatR;
using System.Collections.Generic;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries
{
    public class GetAllCreditAccountsRequest : IRequest<IReadOnlyList<AllCreditAccountsDto>>
    {
    }
}