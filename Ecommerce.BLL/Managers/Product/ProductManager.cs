using AutoMapper;
using FluentValidation; 
using Ecommerce.Common;
using Ecommerce.Common.Pagination;
using Ecommerce.DAL;  

namespace Ecommerce.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductFormDTO> _productFormDTOValidator;
        private readonly IErrorMapper _errorMapper;
        private readonly IMapper _mapper;


        public ProductManager(IUnitOfWork unitOfWork,
            IValidator<ProductFormDTO> productFormDTOValidator,
            IErrorMapper errorMapper , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productFormDTOValidator = productFormDTOValidator;
            _errorMapper = errorMapper;
            _mapper = mapper;
        }
        
         public async Task<GeneralResult<PagedResult<Product>>> GetProductsPaginationAsync
            ( PaginationParameters paginationParameters,  ProductFilterParameters productFilterParameters  )
        {
            var pagedResult = await _unitOfWork.ProductRepository.GetProductsPagination(paginationParameters, productFilterParameters);
            return GeneralResult<PagedResult<Product>>.SuccessResult(pagedResult);
        }
        public async Task<GeneralResult<IEnumerable<ProductListDTO>>> GetProductsAsync()
        {
            var products= await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();
            var productList = _mapper.Map<IEnumerable<ProductListDTO>>(products);
            return GeneralResult<IEnumerable<ProductListDTO>>.SuccessResult(productList);
        }

        public async Task<GeneralResult<ProductDetailsDTO?>> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);
            if (product == null) return GeneralResult<ProductDetailsDTO>.NotFound();
            var productDetails = _mapper.Map<ProductDetailsDTO>(product);
            return GeneralResult<ProductDetailsDTO>.SuccessResult(productDetails);
        }
        
         public async Task<GeneralResult<ProductFormDTO>> CreateProductAsync(ProductFormDTO vm)
        {
            var validationResult = await _productFormDTOValidator.ValidateAsync(vm);
            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<ProductFormDTO>.FailResult(errors);
            }
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(vm.CategoryId);
            if (category is null)
            {
                return GeneralResult<ProductFormDTO>.FailResult("Category Not Found");
            }
            var product = _mapper.Map<Product>(vm);
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductFormDTO>(product);
            return GeneralResult<ProductFormDTO>.SuccessResult(productDto);

        }

        public async Task<GeneralResult<ProductFormDTO>> UpdateProductAsync(int id, ProductFormDTO vm)
        {
            if (id != vm.Id)
                return GeneralResult<ProductFormDTO>.FailResult("Id mismatch");

            var validationResult = await _productFormDTOValidator.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<ProductFormDTO>.FailResult(errors);
            }

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product is null)
                return GeneralResult<ProductFormDTO>.NotFound();

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(vm.CategoryId);

            if (category is null)
                return GeneralResult<ProductFormDTO>.FailResult("Category Not Found");

             _mapper.Map(vm, product);

            product.UpdatedAt = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();

            var productDto = _mapper.Map<ProductFormDTO>(product);

            return GeneralResult<ProductFormDTO>.SuccessResult(productDto);
        }

        public async Task<GeneralResult<bool>> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product is null)
                return GeneralResult<bool>.NotFound();

            _unitOfWork.ProductRepository.Delete(product);

            await _unitOfWork.SaveChangesAsync();

            return GeneralResult<bool>.SuccessResult(true);
        }
    }
}