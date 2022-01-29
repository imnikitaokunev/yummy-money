using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Common;
using Application.Models.Transaction;

namespace Application.Common.Interfaces.Services;

public interface ITransactionService
{
    Task<List<TransactionDto>> GetAsync(GetTransactionsRequest request);
    Task<PaginatedList<TransactionDto>> GetPagedResponseAsync(GetTransactionsWithPaginationRequest request);
    Task<TransactionDto> GetByIdAsync(long id);
    Task<TransactionDto> AddAsync(CreateTransactionRequest request);
    Task UpdateAsync(TransactionDto request);
    Task DeleteAsync(long id);
}
