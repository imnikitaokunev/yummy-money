using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Models.Expense;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service) => _service = service;

        [HttpGet("")]
        public IActionResult Get([FromQuery] ExpenseRequestModel request)
        {
            var expenses = _service.Get(request);
            return new ObjectResult(expenses);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] ExpenseModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = _service.Create(model);

            return Ok(created);
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var expense = _service.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return new ObjectResult(expense);
        }

        [HttpPut("{id:long}")]
        public IActionResult Update([FromRoute] long id, [FromBody] ExpenseModel model)
        {
            if (model == null || id != model.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _service.Update(model);

            return Ok(model);
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var expense = _service.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            _service.Delete(id);

            return Ok();
        }
    }
}