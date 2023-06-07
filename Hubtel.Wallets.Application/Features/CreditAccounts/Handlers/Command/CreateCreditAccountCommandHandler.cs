using AutoMapper;
using Hubtel.Wallets.Application.Contracts.Infrastructure.Mail;
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
    public class CreateCreditAccountCommandHandler : IRequestHandler<CreateCreditAccountCommand, BaseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public CreateCreditAccountCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task<BaseResponse> Handle(CreateCreditAccountCommand request, CancellationToken cancellationToken)
        {
            var payload = request.dto;
            var validationResult = await (new CreateCreditAccountDtoValidator(_unitOfWork)).ValidateAsync(payload);
            if (validationResult.IsValid == false)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }

            var allUserAcounts = await _unitOfWork.AllCreditAccounts.GetCount(x => x.UcaUserIdfk == payload.UcaUserIdfk);
            if (allUserAcounts >= 5)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { "A single user should NOT have more than 5 wallets" }
                };
            }

            // the part of card number
            TblTtype ttype = await _unitOfWork.Ttypes.GetFirst(x => x.TIdpk == request.dto.UcaTypeIdfk);
            if (ttype.TTypeName.ToLower().Replace(@"""""", "'") == "card")
            {
                var first6 = string.Join("", payload.UcaAccountNumber.Take(6));
                var r = first6 + string.Join("", payload.UcaAccountNumber[6..].Select(_ => "x"));
                payload.UcaAccountNumber = r;
            }

            var duplicate = await _unitOfWork.TblUcaUserCreditAccount.GetCount(x => x.UcaUserIdfk == payload.UcaUserIdfk && x.UcaTypeIdfk == payload.UcaTypeIdfk && x.UcaSchemeIdfk == payload.UcaSchemeIdfk && x.UcaAccountNumber.ToLower() == payload.UcaAccountNumber.Trim().ToLower().Replace("'", @""""""));

            if (duplicate > 0)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Creation failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { $"Wallet already exits.{Environment.NewLine}Please enter a new Wallet details" }
                };
            }

            var data = _mapper.Map<TblUcaUserCreditAccount>(payload);
            var lastRecord = await _unitOfWork.TblUcaUserCreditAccount.Add($"EXEC [dbo].[spcUcaUserCreditAccountsInsert] @ucaUserIDfk = {data.UcaUserIdfk}, @ucaTypeIDfk = {data.UcaTypeIdfk}, @ucaSchemeIDfk = {data.UcaSchemeIdfk}, @ucaAccountNumber = {data.UcaAccountNumber}");

            try
            {
                var userEmail = (await _unitOfWork.AllCreditAccounts.GetFirst(x => x.UcaUserIdfk == data.UcaUserIdfk));

                var emil = new Email { Body = EmailTemplates.HtmlEmailTemplate, Subject = EmailTemplates.EmailSubjectForWallet, To = userEmail.Email };

              var mailResponse =  await _emailSender.SendEmail(emil);
                if (mailResponse) Console.WriteLine("Mail sent successfully");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return new BaseResponse
            {
                Id = lastRecord.UcaIdpk,
                Success = true,
                Message = "Created successfully",
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}