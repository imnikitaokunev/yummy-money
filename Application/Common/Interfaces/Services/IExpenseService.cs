using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Models;

namespace Application.Common.Interfaces.Services
{
    public interface IExpenseService
    {
        Task<List<ExpenseDto>> GetAsync(GetExpensesRequest request);
        Task<List<ExpenseWithCategoryDto>> GetWithCategoryAsync(GetExpensesRequest request);
        Task<PaginatedList<ExpenseDto>> GetPagedResponseAsync(GetExpensesWithPaginationRequest request);
        Task<ExpenseDto> GetByIdAsync(long id);
        Task<ExpenseDto> AddAsync(CreateExpenseRequest expense);
        Task UpdateAsync(ExpenseDto expense);
        Task DeleteAsync(long id);
    }
}
