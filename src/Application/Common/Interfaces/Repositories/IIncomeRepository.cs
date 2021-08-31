using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Income;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IIncomeRepository : IRepository<Income, long>
    {
        Task<List<Income>> GetWithCategoryAsync(GetIncomesRequest request);
    }
}
