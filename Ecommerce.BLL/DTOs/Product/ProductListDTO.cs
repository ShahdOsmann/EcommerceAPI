namespace Ecommerce.BLL
{
    public class ProductListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
    }
}