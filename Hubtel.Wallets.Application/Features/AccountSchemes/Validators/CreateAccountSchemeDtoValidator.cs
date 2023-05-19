using FluentValidation;
using Hubtel.Wallets.Application.Contracts.Persistence;
using Hubtel.Wallets.Application.DTOs.AccountSchemes;

namespace Hubtel.Wallets.Application.Features.AccountSchemes.Validators
{
    public class CreateAccountSchemeDtoValidator : AbstractValidator<CreateAccountSchemeDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountSchemeDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.AsTypeIdfk).NotNull().GreaterThanOrEqualTo(1)
                .MustAsync(
                async (id, c) =>
                {
                    return await _unitOfWork.Ttypes.Exists(id);
                }).WithMessage("Invalid account type");

            RuleFor(x => x.AsSchemeName).NotNull().NotEmpty().MinimumLength(1).MaximumLength(150);
        }
    }
}