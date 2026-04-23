namespace Ecommerce.BLL
{
    public class CategoryDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductListDTO> Products { get; set; }
    }
}