using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.BLL;
using Ecommerce.BLL.ViewModels;
using Ecommerce.Common;
using Ecommerce.Common.Pagination;
using Ecommerce.DAL;
 
namespace Ecommerce.Controllers
{
    //[Route("api/v{version:apiVersion}/[controller]")]
    //[ApiController]
    //[ApiVersion("1.0", Deprecated = false)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductManager _productManager;
        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        [Route("Pagination")]
        public async Task<ActionResult<GeneralResult<IEnumerable<Product>>>> GetAllPaginationAsync
           (
               [FromQuery] PaginationParameters paginationParameters,
               [FromQuery] ProductFilterParameters productFilterParameters
            )
        {
            var result = await _productManager.GetProductsPaginationAsync(paginationParameters, productFilterParameters);
            return Ok(result);
        }

        [Authorize]
        [HttpGet] 
        public async Task<ActionResult<GeneralResult<IEnumerable<ProductListDTO>>>> GetAllAsync()
        {
            var products =await  _productManager.GetProductsAsync();
            return Ok(products);
        }
     
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<ProductDetailsDTO>>> GetByIdAsync([FromRoute] int id)
        {
            var result = await _productManager.GetByIdAsync(id);
            if(!result.Success)
                return NotFound(result);  
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<ProductFormDTO>>> CreateAsync(ProductFormDTO product)
        {
            var result = await _productManager.CreateProductAsync(product);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralResult<ProductFormDTO>>> UpdateAsync(  int id,  ProductFormDTO product)
        {
            var result = await _productManager.UpdateProductAsync(id, product);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult<bool>>> DeleteAsync(int id)
        {
            var result = await _productManager.DeleteProductAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}