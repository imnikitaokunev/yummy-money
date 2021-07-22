using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Models;

namespace Application.Common.Interfaces.Services
{
    public interface IIncomeService
    {
        Task<List<IncomeDto>> GetAsync(GetIncomesRequest request);
        Task<PaginatedList<IncomeDto>> GetPagedResponseAsync(GetIncomesWithPaginationRequest request);
        Task<IncomeDto> GetByIdAsync(long id);
        Task<IncomeDto> AddAsync(CreateIncomeRequest income);
        Task UpdateAsync(IncomeDto income);
        Task DeleteAsync(long id);
    }
}
