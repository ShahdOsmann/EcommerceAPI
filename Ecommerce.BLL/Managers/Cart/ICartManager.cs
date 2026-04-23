using Ecommerce.Common;
 

namespace Ecommerce.BLL 
{
    public interface ICartManager
    {
        Task<GeneralResult<CartDTO>> GetUserCartAsync(string userId);
        Task<GeneralResult<bool>> AddToCartAsync(string userId, AddToCartDTO dto);
        Task<GeneralResult<bool>> UpdateCartAsync(string userId, UpdateCartDTO dto);
        Task<GeneralResult<bool>> RemoveFromCartAsync(string userId, int productId);
    }
}
