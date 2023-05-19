using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands;
using Hubtel.Wallets.Application.Features.AccountSchemes.Validators;
using Hubtel.Wallets.Application.Models;
using Hubtel.Wallets.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Handlers.Commands
{
    public class CreateAccountSchemeCommandHandler : IRequestHandler<CreateAccountSchemeCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountSchemeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(CreateAccountSchemeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await (new CreateAccountSchemeDtoValidator(_unitOfWork)).ValidateAsync(request.dto);
            if (validationResult.IsValid == false)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }

            var result = (await _unitOfWork.AccountSchemes.GetCount(x => x.AsTypeIdfk == request.dto.AsTypeIdfk && x.AsSchemeName.ToLower() == request.dto.AsSchemeName.Trim().ToLower().Replace("'", @"""""")));

            if (result > 0)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { $"Record already exists.{Environment.NewLine}Please check the Type and Scheme" }
                };
            }

            var dto = _mapper.Map<TblAsAccountScheme>(request.dto);

            var lastRecord = await _unitOfWork.AccountSchemes.Add($"EXEC [dbo].[spcAsAccountSchemeInsert] @asTypeIDfk = {dto.AsTypeIdfk}, @asSchemeName = {dto.AsSchemeName}"); // returns recently add record

            return new BaseResponse()
            {
                Id = lastRecord.AsIdpk,
                Success = true,
                Message = "Created successfully",
                StatusCode = HttpStatusCode.Created
            };
        }
    }
}