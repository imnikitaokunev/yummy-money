using Microsoft.AspNetCore.Mvc;
using CostAccounting.Core.Models;
using CostAccounting.Services.Services;
using CostAccounting.Services.Models.Category;

namespace CostAccounting.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet("")]
        public IActionResult Get([FromQuery] CategoryRequestModel request)
        {
            var categories = _categoryService.Get(request);
            return new ObjectResult(categories);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CategoryModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = _categoryService.Create(model);

            if (created == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("Create", new {created.Id}, model);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return new ObjectResult(category);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryModel model)
        {
            if (model == null || id != model.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var isUpdated = _categoryService.Update(model);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            var isDeleted = _categoryService.Delete(id);

            if (!isDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}