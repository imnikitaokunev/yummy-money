using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.Models.Transaction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService) =>
            _transactionService = transactionService;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetTransactionsRequest request)
        {
            var transactions = await _transactionService.GetAsync(request);
            return Ok(transactions);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] GetTransactionsWithPaginationRequest request)
        {
            var transactions = await _transactionService.GetPagedResponseAsync(request);
            return Ok(transactions);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateTransactionRequest request)
        {
            var created = await _transactionService.AddAsync(request);
            return CreatedAtAction("GetById", new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromBody] TransactionDto transaction, [FromRoute] long id)
        {
            if (transaction.Id != id)
            {
                return BadRequest("Incorrect id");
            }

            await _transactionService.UpdateAsync(transaction);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            await _transactionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
