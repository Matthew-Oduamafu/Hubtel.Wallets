using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Handlers.Queries
{
    public class GetSingleCreditAccountsForAUserRequestHandler : IRequestHandler<GetSingleCreditAccountsForAUserRequest, AllCreditAccountsForUserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetSingleCreditAccountsForAUserRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AllCreditAccountsForUserDto> Handle(GetSingleCreditAccountsForAUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.VwCreditAccountsForUser.Get($"EXEC [dbo].[spcGetSingleCreditAccountsForUser] @ucaIDpk =  {request.Id}");
            return _mapper.Map<AllCreditAccountsForUserDto>(response);
        }
    }
}