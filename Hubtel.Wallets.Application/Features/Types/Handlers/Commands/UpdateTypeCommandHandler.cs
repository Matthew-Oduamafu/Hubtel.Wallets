using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.Types.Requests.Commands;
using Hubtel.Wallets.Application.Features.Types.Validators;
using Hubtel.Wallets.Application.Models;
using Hubtel.Wallets.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Types.Handlers.Commands
{
    public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = (new UpdateTypeDtoValidator()).Validate(request.dto);
            if (!validationResult.IsValid)
            {
                return new BaseResponse
                {
                    Id = request.dto.TIdpk,
                    Success = false,
                    Message = "Update failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }
            var exists = await _unitOfWork.Ttypes.GetCount(x => x.TTypeName.ToLower() == request.dto.TTypeName.Trim().ToLower().Replace("'", @"""""") && x.TIdpk != request.dto.TIdpk);
            if (exists > 0)
            {
                return new BaseResponse
                {
                    Id = request.dto.TIdpk,
                    Success = false,
                    Message = "Update failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { $"Types '{request.dto.TTypeName}' already assigned to another record.{Environment.NewLine}Enter a new type and try again" }
                };
            }

            var data = _mapper.Map<TblTtype>(request.dto);

            var result = await _unitOfWork.Ttypes.Update($"EXEC [dbo].[spcTTypeUpdate] @tIDpk = {data.TIdpk}, @tTypeName = {data.TTypeName}, @EditedDate = {data.EditedDate}");

            if (result < 1)
            {
                return new BaseResponse
                {
                    Id = request.dto.TIdpk,
                    Success = false,
                    Message = "Update failed",
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Errors = new List<string> { "Item not found" }
                };
            }

            return new BaseResponse
            {
                Id = request.dto.TIdpk,
                Success = true,
                Message = "Updated successfully",
                StatusCode = System.Net.HttpStatusCode.Accepted
            };
        }
    }
}