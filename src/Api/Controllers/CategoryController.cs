using Application.Common.Interfaces.Services;
using Application.Models.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetCategoryRequest request)
        {
            var categories = await _categoryService.GetAsync(request);
            return Ok(categories);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedResponseAsync([FromQuery] GetCategoryWithPaginationRequest request)
        {
            var categories = await _categoryService.GetPagedResponseAsync(request);
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateCategoryRequest category)
        {
            var created = await _categoryService.AddAsync(category);
            return CreatedAtAction("GetById", new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryDto category, [FromRoute] Guid id)
        {
            if (category.Id != id)
            {
                return BadRequest("Incorrect id");
            }

            await _categoryService.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
