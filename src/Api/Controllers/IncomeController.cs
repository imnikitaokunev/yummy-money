using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.Models.Income;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/incomes")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService) => _incomeService = incomeService;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetIncomesRequest request)
        {
            var incomes = await _incomeService.GetWithCategoryAsync(request);
            return Ok(incomes);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            var income = await _incomeService.GetByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateIncomeRequest request)
        {
            var created = await _incomeService.AddAsync(request);
            return CreatedAtAction("GetById", new {id = created.Id}, created);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromBody] IncomeDto income, [FromRoute] long id)
        {
            if (income.Id != id)
            {
                return BadRequest("Incorrect id");
            }

            await _incomeService.UpdateAsync(income);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            await _incomeService.DeleteAsync(id);
            return NoContent();
        }
    }
}