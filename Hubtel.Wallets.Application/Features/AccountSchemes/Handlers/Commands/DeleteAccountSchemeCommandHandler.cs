using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands;
using Hubtel.Wallets.Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Handlers.Commands
{
    public class DeleteAccountSchemeCommandHandler : IRequestHandler<DeleteAccountSchemeCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountSchemeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(DeleteAccountSchemeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
            {
                return new BaseResponse
                {
                    Id = request.Id,
                    Success = false,
                    Message = "Delete failed",
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string> { "Invalid id" }
                };
            }

            var rowsAffected = await _unitOfWork.AccountSchemes.Delete($"EXEC [dbo].[spcAsAccountSchemeDelete] @asIDpk ={request.Id}");

            if (rowsAffected < 1)
            {
                return new BaseResponse
                {
                    Id = request.Id,
                    Success = false,
                    Message = "Delete failed",
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Item not found" }
                };
            }

            return new BaseResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Item removed successfully",
                StatusCode = HttpStatusCode.NoContent
            };
        }
    }
}