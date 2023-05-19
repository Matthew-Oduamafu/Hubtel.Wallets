using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command;
using Hubtel.Wallets.Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Handlers.Command
{
    public class DeleteCreditAccountCommandHandler : IRequestHandler<DeleteCreditAccountCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCreditAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(DeleteCreditAccountCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
            {
                return new BaseResponse
                {
                    Id = request.Id,
                    Message = "Delete failed",
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Id" }
                };
            }

            var result = await _unitOfWork.TblUcaUserCreditAccount.Delete($"EXEC [dbo].[spcUcaUserCreditAccountsDelete] @ucaIDpk = {request.Id}");

            if (result < 1)
            {
                return new BaseResponse
                {
                    Id = request.Id,
                    Message = "Delete failed",
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Errors = new List<string> { "Not found" }
                };
            }

            return new BaseResponse
            {
                Id = request.Id,
                Message = "Deleted successfully",
                Success = true,
                StatusCode = System.Net.HttpStatusCode.NoContent,
            };
        }
    }
}