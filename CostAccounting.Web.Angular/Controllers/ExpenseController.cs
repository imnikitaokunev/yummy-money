using System;
using System.Collections.Generic;
using System.Security.Claims;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Core;
using CostAccounting.Services.Models.Dtos;
using CostAccounting.Services.Models.Error;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Angular.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService) => _expenseService = expenseService;

        // TODO: GetExpensesRequestModel?
        [HttpGet("")]
        public ActionResult<IEnumerable<ExpenseDto>> Get([FromQuery] ExpenseRequestModel request)
        {
            var userId = Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(userId);

            return Ok(_expenseService.Get(request));
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] Expense expense)
        {
            var result = _expenseService.Create(expense);

            // TODO: Mb rename RepositoryResult to RepositoryResponse to have consistent naming?

            if (!result.Success)
            {
                return BadRequest(result.Adapt<RepositoryFailedResponse>());
            }

            return CreatedAtAction("Create", result.Target);
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById([FromRoute] long id)
        {
            var expense = _expenseService.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return new ObjectResult(expense);
        }

        [HttpPut("{id:long}")]
        public IActionResult Update([FromRoute] long id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            var result = _expenseService.Update(expense);

            if (!result.Success)
            {
                return BadRequest(result.Adapt<RepositoryFailedResponse>());
            }

            return Ok(result.Target);
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var expense = _expenseService.GetById(id);

            if (expense == null)
            {
                return NotFound();
            }

            var result = _expenseService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(result.Adapt<RepositoryFailedResponse>());
            }

            return NoContent();
        }
    }
}