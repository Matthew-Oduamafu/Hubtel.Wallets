using FluentValidation;
using Hubtel.Wallets.Application.DTOs.Types;

namespace Hubtel.Wallets.Application.Features.Types.Validators
{
    public class UpdateTypeDtoValidator : AbstractValidator<UpdateTypeDto>
    {
        public UpdateTypeDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            Include(new CreateTypeDtoValidator());

            RuleFor(x => x.TIdpk).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(x => x.EditedDate).NotNull();
        }
    }
}