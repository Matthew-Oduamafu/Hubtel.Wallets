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
    public class GetAllAccountSchemesRequestHandler : IRequestHandler<GetAllAccountSchemesRequest, IReadOnlyList<AccountSchemeGetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountSchemesRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AccountSchemeGetDto>> Handle(GetAllAccountSchemesRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.AccountSchemesGet.GetAll($"EXEC [dbo].[spcAsAccountSchemeGetAll]");
            return _mapper.Map<IReadOnlyList<AccountSchemeGetDto>>(response);
        }
    }
}