
using FluentValidation;
using Schema;

namespace WebAPI.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Full name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
            RuleFor(x=>x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords must match");

             When(x => x.RoleId == 2, () =>
            {
                RuleFor(x => x.Iban).NotEmpty().WithMessage("IBAN is required for Personnel.").Matches(@"^TR\d{24}$").WithMessage("IBAN must start with 'TR' followed by 24 digits");
            });
        }
        
    }
}