using Hubtel.Wallets.Application.Contracts.Identity;
using Hubtel.Wallets.Application.Features.Accounts.Requests;
using Hubtel.Wallets.Application.Features.Accounts.Validators;
using Hubtel.Wallets.Application.Models.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hubtel.Wallets.Application.Contracts.Persistence;

namespace Hubtel.Wallets.Application.Features.Accounts.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IRegistrationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IRegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = (new RegistrationRequestValidator()).Validate(request.RegistrationRequest);
            if (validationResult.IsValid == false)
            {
                return new RegistrationBadResponse
                {
                    Message = "Registration failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }
            try
            {
                var response = await _unitOfWork.ApplicationUser.Register(request.RegistrationRequest);
                return response;
            }
            catch (Exception ex)
            {
                return new RegistrationBadResponse
                {
                    Message = "Registration failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}