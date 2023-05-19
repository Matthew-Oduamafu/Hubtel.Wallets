using Hubtel.Wallets.Application.DTOs.Types;
using MediatR;

namespace Hubtel.Wallets.Application.Features.Types.Requests.Queries
{
    public class GetSingleTypesRequest : IRequest<TypeDto>
    {
        public int Id { get; set; }
    }
}