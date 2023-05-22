using FluentValidation;
using Hubtel.Wallets.Application.Models.Identity;
using System.Text.RegularExpressions;

namespace Hubtel.Wallets.Application.Features.Accounts.Validators
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6).Must(x =>
            {
                Regex rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
                return rgx.IsMatch(x);
            }).WithMessage("Password must be minimum 6 characters, at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character");
        }
    }
}