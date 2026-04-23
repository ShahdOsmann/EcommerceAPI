using FluentValidation;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required.")
                .WithErrorCode("ERR-01")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters.")
                .WithErrorCode("ERR-02")
                .MaximumLength(50)
                .WithMessage("Username cannot exceed 50 characters.")
                .WithErrorCode("ERR-03");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .WithErrorCode("ERR-04")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .WithErrorCode("ERR-05");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .WithErrorCode("ERR-06")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters.")
                .WithErrorCode("ERR-07");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .WithErrorCode("ERR-08")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters.")
                .WithErrorCode("ERR-09");

            RuleFor(x => x.LastName)
                .MaximumLength(50)
                .WithMessage("Last name cannot exceed 50 characters.")
                .WithErrorCode("ERR-10");
        }
    }
}