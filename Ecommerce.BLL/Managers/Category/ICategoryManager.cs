 using Ecommerce.Common;

namespace Ecommerce.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<CategoryListDTO>>> GetAllAsync();
        Task<GeneralResult<CategoryDetailsDTO>> GetByIdAsync(int id);
        Task<GeneralResult<CategoryFormDTO>> CreateAsync(CategoryFormDTO dto);
        Task<GeneralResult<CategoryFormDTO>> UpdateAsync(int id, CategoryFormDTO dto);
        Task<GeneralResult<bool>> DeleteAsync(int id);
    }
}