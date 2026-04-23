
using FluentValidation;

namespace Ecommerce.BLL
{
    public class CategoryFormDTOValidator : AbstractValidator<CategoryFormDTO>
    {
        public CategoryFormDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");
        }
    }
}