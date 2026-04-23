 using Ecommerce.Common;
using Ecommerce.Common.Pagination;
using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public interface IProductManager
    {
        Task<GeneralResult<PagedResult<Product>>> GetProductsPaginationAsync
            (PaginationParameters paginationParameters, ProductFilterParameters productFilterParameters);
        Task<GeneralResult<IEnumerable<ProductListDTO>>> GetProductsAsync();
        Task<GeneralResult<ProductDetailsDTO?>> GetByIdAsync(int id);
        Task<GeneralResult<ProductFormDTO>> CreateProductAsync(ProductFormDTO vm);
        Task<GeneralResult<ProductFormDTO>> UpdateProductAsync(int id, ProductFormDTO vm);

        Task<GeneralResult<bool>> DeleteProductAsync(int id);
    }
}