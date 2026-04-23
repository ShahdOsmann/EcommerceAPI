 

namespace Ecommerce.BLL 
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemDTO> Items { get; set; }
    }
}
