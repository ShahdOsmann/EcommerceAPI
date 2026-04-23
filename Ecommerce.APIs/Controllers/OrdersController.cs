using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ecommerce.BLL;
using Ecommerce.BLL.ViewModels;
using Ecommerce.Common;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<int>>> Create(CreateOrderDTO dto)
        {
            var result = await _orderManager.CreateOrderAsync(GetUserId(), dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<OrderDTO>>>> GetMyOrders()
        {
            var result = await _orderManager.GetUserOrdersAsync(GetUserId());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<OrderDTO>>> GetById(int id)
        {
            var result = await _orderManager.GetOrderByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}