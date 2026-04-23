
using Ecommerce.Common;
using Ecommerce.Common.Pagination;

namespace Ecommerce.DAL
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();

        Task<Product?> GetByIdWithCategoryAsync(int productId);
        Task<PagedResult<Product>> GetProductsPagination
           (PaginationParameters? paginationParameters = null, ProductFilterParameters? productFilterParameters = null);
    }
}
