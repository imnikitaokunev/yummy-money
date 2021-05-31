using System;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Core;
using CostAccounting.Services.Models.Error;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Angular.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service) => _categoryService = service;

        [HttpGet("")]
        public IActionResult Get([FromQuery] CategoryRequestModel request)
        {
            var categories = _categoryService.Get(request);
            return new ObjectResult(categories);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] Category category)
        {
            var result = _categoryService.Create(category);

            if (!result.Success)
            {
                return BadRequest(result.Adapt<RepositoryFailedResponse>());
            }

            return CreatedAtAction("Create", result.Target);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return new ObjectResult(category);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var response = _categoryService.Update(category);

            if (!response.Success)
            {
                return BadRequest(response.Adapt<RepositoryFailedResponse>());
            }

            return Ok(response.Target);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            var response = _categoryService.Delete(id);

            if (!response.Success)
            {
                return BadRequest(response.Adapt<RepositoryFailedResponse>());
            }

            return NoContent();
        }
    }
}