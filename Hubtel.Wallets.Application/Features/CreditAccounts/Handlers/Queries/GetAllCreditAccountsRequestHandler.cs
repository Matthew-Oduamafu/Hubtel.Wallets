using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Handlers.Queries
{
    public class GetAllCreditAccountsRequestHandler : IRequestHandler<GetAllCreditAccountsRequest, IReadOnlyList<AllCreditAccountsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCreditAccountsRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AllCreditAccountsDto>> Handle(GetAllCreditAccountsRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.AllCreditAccounts.GetAll($"EXEC [dbo].[spcUcaUserCreditAccountsGetAll]");
            return _mapper.Map<IReadOnlyList<AllCreditAccountsDto>>(response);
        }
    }
}