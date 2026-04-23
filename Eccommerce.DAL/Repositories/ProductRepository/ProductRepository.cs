 
using Microsoft.EntityFrameworkCore;
using Ecommerce.Common;
using Ecommerce.Common.Pagination; 


namespace Ecommerce.DAL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context)
        { 
        }
        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int productId)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<PagedResult<Product>> GetProductsPagination
           (
               PaginationParameters? paginationParameters = null,
               ProductFilterParameters? productFilterParameters = null
           )
        {
            IQueryable<Product> query = _context.Set<Product>().AsQueryable();

            query.Include(e => e.Category);

            if (productFilterParameters != null)
            {
                query = ApplyFilter(query, productFilterParameters);
            }

            // Total Count
            var totalCount = await query.CountAsync();

            var pageNumber = paginationParameters?.PageNumber ?? 1;
            var pageSize = paginationParameters?.PageSize ?? totalCount;

            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 50);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<Product>
            {
                Items = items,
                Metadata = new PaginationMetadata
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    HasNext = pageNumber < totalPages,
                    HasPrevious = pageNumber > totalPages,
                }
            };
        }
        private IQueryable<Product> ApplyFilter(IQueryable<Product> query, ProductFilterParameters productFilterParameters)
        {

            if (productFilterParameters.MinPrice > 0)
            {
                query = query.Where(e => e.Price > productFilterParameters.MinPrice);
            }

            if (productFilterParameters.MaxPrice > 0)
            {
                query = query.Where(e => e.Price < productFilterParameters.MaxPrice);
            }

            if (productFilterParameters.MinCount > 0)
            {
                query = query.Where(e => e.Count > productFilterParameters.MinCount);
            }

            if (productFilterParameters.MaxCount > 0)
            {
                query = query.Where(e => e.Count < productFilterParameters.MaxCount);
            }

            if (!string.IsNullOrEmpty(productFilterParameters.Search))
            {
                query = query.Where(e => e.Title.Contains(productFilterParameters.Search));
            }

            return query;
        }
    }
}
