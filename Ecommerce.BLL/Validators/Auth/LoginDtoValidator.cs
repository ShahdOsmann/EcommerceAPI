using FluentValidation;

namespace Ecommerce.BLL
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .WithErrorCode("ERR-01")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .WithErrorCode("ERR-02");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .WithErrorCode("ERR-03")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters.")
                .WithErrorCode("ERR-04");
        }
    }
}