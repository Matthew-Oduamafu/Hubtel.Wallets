using FluentValidation;
using Hubtel.Wallets.Application.DTOs.Types;

namespace Hubtel.Wallets.Application.Features.Types.Validators
{
    public class CreateTypeDtoValidator : AbstractValidator<CreateTypeDto>
    {
        public CreateTypeDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.TTypeName).NotNull().MinimumLength(1).MaximumLength(100);
        }
    }
}