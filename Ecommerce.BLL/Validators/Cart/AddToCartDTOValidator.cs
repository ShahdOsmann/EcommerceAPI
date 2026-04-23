using FluentValidation;

namespace Ecommerce.BLL
{
    public class AddToCartDTOValidator : AbstractValidator<AddToCartDTO>
    {
        public AddToCartDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Invalid ProductId");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}