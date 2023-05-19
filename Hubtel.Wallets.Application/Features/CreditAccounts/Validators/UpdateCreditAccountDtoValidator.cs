using FluentValidation;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.CreditAccounts;

namespace Hubtel.Wallets.Application.Features.CreditAccounts.Validators
{
    public class UpdateCreditAccountDtoValidator : AbstractValidator<UpdateCreditAccountDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCreditAccountDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            Include(new CreateCreditAccountDtoValidator(unitOfWork));

            RuleFor(x => x.UcaIdpk).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.UcaEditedDate).NotNull();
        }
    }
}