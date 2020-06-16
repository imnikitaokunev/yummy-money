using System;
using Microsoft.AspNetCore.Mvc;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Models.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CostAccounting.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _service.Create(model);

            return CreatedAtAction("Create", model);
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
            if (model == null || id != model.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _service.Update(model);

            return Ok(model);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var category = _service.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            _service.Delete(id);

            return Ok();
        }
    }
}