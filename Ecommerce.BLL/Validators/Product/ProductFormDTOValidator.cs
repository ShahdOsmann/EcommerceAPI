using FluentValidation;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class ProductFormDTOValidator : AbstractValidator<ProductFormDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductFormDTOValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(e => e.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty.")
                .WithErrorCode("ERR-01")

                .MinimumLength(3)
                .WithMessage("Title at least 3 characters")
                .WithErrorCode("ERR-02")

                .MaximumLength(100)
                .WithMessage("Title cannot be longer than 100 characters")
                .WithErrorCode("ERR-03")

                .MustAsync(CheckTitleIsUnique)
                .WithMessage("Title already exists")
                .WithErrorCode("ERR-04");

            RuleFor(e => e.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0")
                .WithErrorCode("ERR-05");

            RuleFor(e => e.Count)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("ERR-07");
        }

        private async Task<bool> CheckTitleIsUnique(string title, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return !products.Any(e => e.Title == title);
        }
    }
}