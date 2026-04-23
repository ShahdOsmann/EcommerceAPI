 namespace Ecommerce.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProducts(int id);

    }
}
