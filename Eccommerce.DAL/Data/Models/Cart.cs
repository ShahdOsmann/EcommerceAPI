 

namespace Ecommerce.DAL 
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();
    }
}
