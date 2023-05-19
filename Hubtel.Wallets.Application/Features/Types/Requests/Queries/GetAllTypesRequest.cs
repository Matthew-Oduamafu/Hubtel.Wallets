using Hubtel.Wallets.Application.DTOs.Types;
using MediatR;
using System.Collections.Generic;

namespace Hubtel.Wallets.Application.Features.Types.Requests.Queries
{
    public class GetAllTypesRequest : IRequest<IReadOnlyList<TypeDto>>
    {
    }
}