using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.Features.Accounts.Requests;
using Hubtel.Wallets.Application.Features.Accounts.Validators;
using Hubtel.Wallets.Application.Models.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Features.Accounts.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, IAuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IAuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = (new AuthRequestValidator()).Validate(request.AuthRequest);
            if (validationResult.IsValid == false)
            {
                return new AuthBadResponse
                {
                    Message = "Login failed",
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Errors = validationResult.Errors.ConvertAll(x => x.ErrorMessage)
                };
            }

            try
            {
                var response = await _unitOfWork.ApplicationUser.Login(request.AuthRequest);
                return response;
            }
            catch (Exception ex)
            {
                return new AuthBadResponse
                {
                    Message = "Login failed",
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}