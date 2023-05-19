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
    public class GetAllAccountSchemesByTypeRequestHandler : IRequestHandler<GetAllAccountSchemesByTypeRequest, IReadOnlyList<AccountSchemeGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountSchemesByTypeRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AccountSchemeGetDto>> Handle(GetAllAccountSchemesByTypeRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.AccountSchemesGet.GetAll($"EXEC [dbo].[spcGetAllSchemeByType] @asTypeIDfk = {request.TypeId}");
            return _mapper.Map<IReadOnlyList<AccountSchemeGetDto>>(response);
        }
    }
}