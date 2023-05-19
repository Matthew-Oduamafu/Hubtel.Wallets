using FluentValidation;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.AccountSchemes;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Validators
{
    public class UpdateAccountSchemeDtoValidator : AbstractValidator<UpdateAccountSchemeDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountSchemeDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            Include(new CreateAccountSchemeDtoValidator(unitOfWork));

            RuleFor(x => x.AsIdpk).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.EditedDate).NotNull();
        }
    }
}