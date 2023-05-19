using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Handlers.Queries
{
    public class GetAllTypesAndSchemesRequestHandler : IRequestHandler<GetAllTypesAndSchemesRequest, IReadOnlyList<VwTnsTypeAndSchemeGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTypesAndSchemesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<VwTnsTypeAndSchemeGetDto>> Handle(GetAllTypesAndSchemesRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.VwTnsTypeAndSchemes.GetAll($"EXEC [dbo].[spcGetAllTypesAndSchemes]");
            return _mapper.Map<IReadOnlyList<VwTnsTypeAndSchemeGetDto>>(response);
        }
    }
}