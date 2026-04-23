 
namespace Ecommerce.BLL
{
    public class ProductFormDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public DateOnly ExpiryDate { get; set; }

        public int CategoryId { get; set; }

         public string? ImageUrl { get; set; }

  
        public List<CategoryListDTO>? Categories { get; set; }
    }
}