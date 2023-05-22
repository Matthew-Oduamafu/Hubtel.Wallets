using FluentValidation;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.CreditAccounts;
using System;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Validators
{
    public class CreateCreditAccountDtoValidator : AbstractValidator<CreateCreditAccountDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCreditAccountDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.UcaUserIdfk).NotNull().NotEmpty().UnlessAsync(async (x, c) =>
            {
                var result = await _unitOfWork.ApplicationUser.UserExistsByIdAsync(x.UcaUserIdfk);
                return result;
            }).WithMessage("Invalid user Id");

            RuleFor(x => x.UcaTypeIdfk).NotNull().GreaterThanOrEqualTo(1)
                .MustAsync(async (id, c) =>
                {
                    return await _unitOfWork.Ttypes.Exists(id);
                }).WithMessage($"Invalid Type.{Environment.NewLine}Please enter a new Type and try again");

            RuleFor(x => x.UcaSchemeIdfk).NotNull().GreaterThanOrEqualTo(1)
                .MustAsync(async (id, c) =>
                {
                    return await _unitOfWork.AccountSchemes.Exists(id);
                }).WithMessage($"Invalid Scheme.{Environment.NewLine}Please enter a new Scheme and try again");

            RuleFor(x => x.UcaAccountNumber).NotNull().MinimumLength(1).MaximumLength(150);
        }
    }
}