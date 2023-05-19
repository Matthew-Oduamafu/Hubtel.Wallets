using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.AccountSchemes;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Handlers.Queries
{
    public class GetSingleAccountSchemeRequestHandler : IRequestHandler<GetSingleAccountSchemeRequest, AccountSchemeGetDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleAccountSchemeRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountSchemeGetDto> Handle(GetSingleAccountSchemeRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.AccountSchemesGet.Get($"EXEC [dbo].[spcAsAccountSchemeGetSingle] @asIDpk = {request.Id}");
            return _mapper.Map<AccountSchemeGetDto>(response);
        }
    }
}