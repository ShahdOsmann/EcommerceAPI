using Microsoft.AspNetCore.Mvc;
using Ecommerce.BLL;
using Ecommerce.BLL.ViewModels;
using Ecommerce.Common;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult<IEnumerable<CategoryListDTO>>>> GetAll()
        {
            var result = await _categoryManager.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryDetailsDTO>>> GetById(int id)
        {
            var result = await _categoryManager.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResult<CategoryFormDTO>>> Create(CategoryFormDTO dto)
        {
            var result = await _categoryManager.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralResult<CategoryFormDTO>>> Update(int id, CategoryFormDTO dto)
        {
            var result = await _categoryManager.UpdateAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResult<bool>>> Delete(int id)
        {
            var result = await _categoryManager.DeleteAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}