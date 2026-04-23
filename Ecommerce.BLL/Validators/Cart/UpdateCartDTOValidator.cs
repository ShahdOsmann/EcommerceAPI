 
using FluentValidation;

namespace Ecommerce.BLL
{
    public class UpdateCartDTOValidator : AbstractValidator<UpdateCartDTO>
    {
        public UpdateCartDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative");
        }
    }
}