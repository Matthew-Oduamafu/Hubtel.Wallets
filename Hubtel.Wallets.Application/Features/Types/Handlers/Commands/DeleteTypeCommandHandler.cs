using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.Types.Requests.Commands;
using Hubtel.Wallets.Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Types.Handlers.Commands
{
    public class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
            {
                return new BaseResponse
                {
                    Id = request.Id,
                    Success = false,
                    Message = "Delete failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid Id" }
                };
            }
            var result = await _unitOfWork.Ttypes.Delete($"EXEC [dbo].[spcTTypeDelete] @tIDpk = {request.Id}");

            if (result < 1)
            {
                return new BaseResponse
                {
                    Id = request.Id,
                    Success = false,
                    Message = "Delete failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { "No record found" }
                };
            }

            return new BaseResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Deleted successfully",
                StatusCode = System.Net.HttpStatusCode.NoContent
            };
        }
    }
}