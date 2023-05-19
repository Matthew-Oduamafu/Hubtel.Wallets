using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.Types;
using Hubtel.Wallets.Application.Features.Types.Requests.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Types.Handlers.Queries
{
    public class GetSingleTypesRequestHandler : IRequestHandler<GetSingleTypesRequest, TypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleTypesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TypeDto> Handle(GetSingleTypesRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.TtypesGet.Get($"EXEC [dbo].[spcTTypeGetSingle] @tIDpk = {request.Id}");
            return _mapper.Map<TypeDto>(response);
        }
    }
}