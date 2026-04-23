using AutoMapper;
using FluentValidation;
using Ecommerce.BLL.ViewModels;
using Ecommerce.Common;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryFormDTO> _validator;
        private readonly IErrorMapper _errorMapper;

        public CategoryManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CategoryFormDTO> validator,
            IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<IEnumerable<CategoryListDTO>>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryListDTO>>(categories);
            return GeneralResult<IEnumerable<CategoryListDTO>>.SuccessResult(result);
        }

        public async Task<GeneralResult<CategoryDetailsDTO>> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryWithProducts(id);

            if (category == null)
                return GeneralResult<CategoryDetailsDTO>.NotFound();

            var result = _mapper.Map<CategoryDetailsDTO>(category);
            return GeneralResult<CategoryDetailsDTO>.SuccessResult(result);
        }

        public async Task<GeneralResult<CategoryFormDTO>> CreateAsync(CategoryFormDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<CategoryFormDTO>.FailResult(errors);
            }

            var category = _mapper.Map<Category>(dto);

            _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();

            return GeneralResult<CategoryFormDTO>.SuccessResult(dto);
        }

        public async Task<GeneralResult<CategoryFormDTO>> UpdateAsync(int id, CategoryFormDTO dto)
        {
            if (id != dto.Id)
                return GeneralResult<CategoryFormDTO>.FailResult("Id mismatch");

            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<CategoryFormDTO>.FailResult(errors);
            }

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return GeneralResult<CategoryFormDTO>.NotFound();

            _mapper.Map(dto, category);

            await _unitOfWork.SaveChangesAsync();

            return GeneralResult<CategoryFormDTO>.SuccessResult(dto);
        }

        public async Task<GeneralResult<bool>> DeleteAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return GeneralResult<bool>.NotFound();

            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return GeneralResult<bool>.SuccessResult(true);
        }
    }
}