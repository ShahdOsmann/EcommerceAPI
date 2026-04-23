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
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<CartDTO>>> GetCart()
        {
            var result = await _cartManager.GetUserCartAsync(GetUserId());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<bool>>> Add(AddToCartDTO dto)
        {
            var result = await _cartManager.AddToCartAsync(GetUserId(), dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GeneralResult<bool>>> Update(UpdateCartDTO dto)
        {
            var result = await _cartManager.UpdateCartAsync(GetUserId(), dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult<GeneralResult<bool>>> Remove(int productId)
        {
            var result = await _cartManager.RemoveFromCartAsync(GetUserId(), productId);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}