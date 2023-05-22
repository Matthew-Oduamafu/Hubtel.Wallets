using FluentValidation;
using Hubtel.Wallets.Application.Models.Identity;
using System.Text.RegularExpressions;

namespace Hubtel.Wallets.Application.Features.Accounts.Validators
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            Include(new AuthRequestValidator());

            RuleFor(x => x.FirstName).NotNull().MinimumLength(3).MaximumLength(150);
            RuleFor(x => x.LastName).NotNull().MinimumLength(3).MaximumLength(150);
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(10).Must(x =>
            {
                Regex rgx = new Regex(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$");
                return rgx.IsMatch(x);
            }).WithMessage("Invalid phone number");
        }
    }
}