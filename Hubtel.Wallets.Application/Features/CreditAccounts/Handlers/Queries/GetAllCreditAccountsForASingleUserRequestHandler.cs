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
    public class GetAllCreditAccountsForASingleUserRequestHandler : IRequestHandler<GetAllCreditAccountsForASingleUserRequest, IReadOnlyList<AllCreditAccountsForUserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCreditAccountsForASingleUserRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AllCreditAccountsForUserDto>> Handle(GetAllCreditAccountsForASingleUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _unitOfWork.VwCreditAccountsForUser.GetAll($"EXEC [dbo].[spcGetAllCreditAccountsForUser] @Email = {request.Email}, @userId = {request.UserId}");
            return _mapper.Map<IReadOnlyList<AllCreditAccountsForUserDto>>(response);
        }
    }
}