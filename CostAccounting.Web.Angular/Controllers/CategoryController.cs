using System;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Core;
using CostAccounting.Services.Models.Category;
using CostAccounting.Services.Models.Error;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service) => _service = service;

        [HttpGet("")]
        public IActionResult Get([FromQuery] CategoryRequestModel request)
        {
            var categories = _service.Get(request);
            return new ObjectResult(categories);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CategoryModel model)
        {
            var response = _service.Create(model);

            // TODO: Error handling middleware or RepositoryResponse?

            if (!response.Success)
            {
                return BadRequest(response.Adapt<RepositoryFailedResponse>());
            }

            return CreatedAtAction("Create", response.Target);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var category = _service.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CategoryModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var response = _service.Update(model);

            if (!response.Success)
            {
                return BadRequest(response.Adapt<RepositoryFailedResponse>());
            }

            return Ok(response.Target);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var category = _service.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            var response = _service.Delete(id);

            if (!response.Success)
            {
                return BadRequest(response.Adapt<RepositoryFailedResponse>());
            }

            return NoContent();
        }
    }
}