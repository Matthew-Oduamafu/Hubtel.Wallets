using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.Types;
using Hubtel.Wallets.Application.Features.Types.Requests.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Types.Handlers.Queries
{
    public class GetAllTypesRequestHandler : IRequestHandler<GetAllTypesRequest, IReadOnlyList<TypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTypesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<TypeDto>> Handle(GetAllTypesRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.TtypesGet.GetAll($"EXEC [dbo].[spcTTypeGetAll]");
            return _mapper.Map<IReadOnlyList<TypeDto>>(response);
        }
    }
}