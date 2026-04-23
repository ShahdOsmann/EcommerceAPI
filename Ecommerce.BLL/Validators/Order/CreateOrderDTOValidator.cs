using FluentValidation;

namespace Ecommerce.BLL
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator()
        {
            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("Shipping address is required")
                .MinimumLength(10).WithMessage("Address too short")
                .MaximumLength(200).WithMessage("Address too long");
        }
    }
}