using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.CreditAccounts.Requests.Command;
using Hubtel.Wallets.Application.Features.CreditAccounts.Validators;
using Hubtel.Wallets.Application.Models;
using Hubtel.Wallets.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Handlers.Command
{
    public class UpdateCreditAccountCommandHandler : IRequestHandler<UpdateCreditAccountCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCreditAccountCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(UpdateCreditAccountCommand request, CancellationToken cancellationToken)
        {
            var payload = request.dto;
            var validationResult = await (new UpdateCreditAccountDtoValidator(_unitOfWork)).ValidateAsync(payload);
            if (validationResult.IsValid == false)
            {
                return new BaseResponse
                {
                    Id = payload.UcaIdpk,
                    Message = "Update failed",
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }

            // the part of card number
            TblTtype ttype = await _unitOfWork.Ttypes.Get(x => x.TIdpk == request.dto.UcaTypeIdfk);
            if (ttype.TTypeName.ToLower().Replace(@"""""", "'") == "card")
            {
                var first6 = string.Join("", payload.UcaAccountNumber[..6]);
                var r = first6 + string.Join("", payload.UcaAccountNumber[6..].Select(_ => "x"));
                payload.UcaAccountNumber = r;
            }

            var duplicate = await _unitOfWork.TblUcaUserCreditAccount.GetCount(x => x.UcaUserIdfk == payload.UcaUserIdfk && x.UcaTypeIdfk == payload.UcaTypeIdfk && x.UcaSchemeIdfk == payload.UcaSchemeIdfk && x.UcaAccountNumber.ToLower() == payload.UcaAccountNumber.Trim().ToLower().Replace("'", @"""""") && x.UcaIdpk != payload.UcaIdpk);
            if (duplicate > 0)
            {
                return new BaseResponse
                {
                    Id = payload.UcaIdpk,
                    Message = "Update failed",
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { $"Wallet already exits.{Environment.NewLine}Please enter a new Wallet details" }
                };
            }

            var data = _mapper.Map<TblUcaUserCreditAccount>(payload);
            var result = await _unitOfWork.TblUcaUserCreditAccount.Update($"EXEC [dbo].[spcUcaUserCreditAccountsUpdate] @ucaIDpk = {data.UcaIdpk}, @ucaUserIDfk = {data.UcaUserIdfk}, @ucaTypeIDfk = {data.UcaTypeIdfk}, @ucaSchemeIDfk = {data.UcaSchemeIdfk}, @ucaAccountNumber = {data.UcaAccountNumber}, @ucaEditedDate = {data.UcaEditedDate}");

            if (result < 1)
            {
                return new BaseResponse
                {
                    Id = payload.UcaIdpk,
                    Message = "Update failed",
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Errors = new List<string> { "Not found" }
                };
            }

            return new BaseResponse
            {
                Id = payload.UcaIdpk,
                Message = "Updated successfully",
                Success = true,
                StatusCode = System.Net.HttpStatusCode.Accepted,
            };
        }
    }
}