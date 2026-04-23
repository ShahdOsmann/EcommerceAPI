using Ecommerce.Common; 

namespace Ecommerce.BLL 
{
    public interface IOrderManager
    {
        Task<GeneralResult<int>> CreateOrderAsync(string userId, CreateOrderDTO dto);
        Task<GeneralResult<IEnumerable<OrderDTO>>> GetUserOrdersAsync(string userId);
        Task<GeneralResult<OrderDTO>> GetOrderByIdAsync(int id);
    }
}
