using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.Models.Expense;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService) => _expenseService = expenseService;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetExpensesRequest request)
        {
            var expenses = await _expenseService.GetWithCategoryAsync(request);
            return Ok(expenses);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateExpenseRequest expense)
        {
            var created = await _expenseService.AddAsync(expense);
            return CreatedAtAction("GetById", new {id = created.Id}, created);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ExpenseDto expense, [FromRoute] long id)
        {
            if (expense.Id != id)
            {
                return BadRequest("Incorrect id");
            }

            await _expenseService.UpdateAsync(expense);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            await _expenseService.DeleteAsync(id);
            return NoContent();
        }
    }
}