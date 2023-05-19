using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.AccountSchemes.Requests.Commands;
using Hubtel.Wallets.Application.Features.AccountSchemes.Validators;
using Hubtel.Wallets.Application.Models;
using Hubtel.Wallets.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Handlers.Commands
{
    public class UpdateAccountSchemeCommandHandler : IRequestHandler<UpdateAccountSchemeCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountSchemeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(UpdateAccountSchemeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await (new UpdateAccountSchemeDtoValidator(_unitOfWork)).ValidateAsync(request.dto);

            if (validationResult.IsValid == false)
            {
                return new BaseResponse
                {
                    Id = request.dto.AsIdpk,
                    Success = false,
                    Message = "Update failed",
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }

            Expression<Func<TblAsAccountScheme, bool>> predicate = x => x.AsTypeIdfk == request.dto.AsTypeIdfk && x.AsSchemeName.ToLower() == request.dto.AsSchemeName.Trim().ToLower().Replace("'", @"""""") && x.AsIdpk != request.dto.AsIdpk;

            var result = (await _unitOfWork.AccountSchemes.GetCount(predicate));

            if (result > 0)
            {
                return new BaseResponse()
                {
                    Id = request.dto.AsIdpk,
                    Success = false,
                    Message = "Update failed",
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string>() { $"Record already exists.{Environment.NewLine}Please check the Type and Scheme" }
                };
            }

            var dto = _mapper.Map<TblAsAccountScheme>(request.dto);
            var rowsAffected = await _unitOfWork.AccountSchemes.Update($"EXEC [dbo].[spcAsAccountSchemeUpdate] @asIDpk = {dto.AsIdpk}, @asTypeIDfk = {dto.AsTypeIdfk}, @asSchemeName = {dto.AsSchemeName}, @EditedDate = {dto.EditedDate}");

            if (rowsAffected < 1)
            {
                return new BaseResponse
                {
                    Id = request.dto.AsIdpk,
                    Success = false,
                    Message = "Update failed",
                    StatusCode = HttpStatusCode.NotFound,
                    Errors = new List<string> { "Item not found" }
                };
            }

            return new BaseResponse
            {
                Id = request.dto.AsIdpk,
                Success = true,
                Message = "Updated successfully",
                StatusCode = HttpStatusCode.Accepted
            };
        }
    }
}