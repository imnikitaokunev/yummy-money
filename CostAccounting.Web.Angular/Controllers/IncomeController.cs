using System;
using System.Collections.Generic;
using System.Security.Claims;
using CostAccounting.Core.Entities.Core;
using CostAccounting.Core.Models.Core;
using CostAccounting.Services.Core;
using CostAccounting.Services.Models.Dtos;
using CostAccounting.Services.Models.Error;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Angular.Controllers
{
    [Route("api/incomes/")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<IncomeDto>> Get([FromQuery] IncomeRequestModel request)
        {
            var userId = Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //request.UserId = Guid.Parse(userId);

            return Ok(_incomeService.Get(request));
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] Income expense)
        {
            var result = _incomeService.Create(expense);

            // TODO: Mb rename RepositoryResult to RepositoryResponse to have consistent naming?

            if (!result.Success) return BadRequest(result.Adapt<RepositoryFailedResponse>());

            return CreatedAtAction("Create", result.Target);
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById([FromRoute] long id)
        {
            var expense = _incomeService.GetById(id);

            if (expense == null) return NotFound();

            return new ObjectResult(expense);
        }

        [HttpPut("{id:long}")]
        public IActionResult Update([FromRoute] long id, [FromBody] Income expense)
        {
            if (id != expense.Id) return BadRequest();

            var result = _incomeService.Update(expense);

            if (!result.Success) return BadRequest(result.Adapt<RepositoryFailedResponse>());

            return Ok(result.Target);
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var expense = _incomeService.GetById(id);

            if (expense == null) return NotFound();

            var result = _incomeService.Delete(id);

            if (!result.Success) return BadRequest(result.Adapt<RepositoryFailedResponse>());

            return NoContent();
        }
    }
}