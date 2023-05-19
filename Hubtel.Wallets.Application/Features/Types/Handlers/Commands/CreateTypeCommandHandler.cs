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
    public class CreateTypeCommandHandler : IRequestHandler<CreateTypeCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = (new CreateTypeDtoValidator()).Validate(request.dto);
            if (!validationResult.IsValid)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }

            var exists = await _unitOfWork.Ttypes.GetCount(x => x.TTypeName.ToLower() == request.dto.TTypeName.Trim().ToLower().Replace("'", @""""""));
            if (exists > 0)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { $"Account type already exists.{Environment.NewLine}Please enter a new type and try again" }
                };
            }

            var data = _mapper.Map<TblTtype>(request.dto);
            var result = await _unitOfWork.Ttypes.Add($"EXEC [dbo].[spcTTypeInsert] @tTypeName = {data.TTypeName}");

            return new BaseResponse
            {
                Id = result.TIdpk,
                Success = true,
                Message = "Created successfully",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}