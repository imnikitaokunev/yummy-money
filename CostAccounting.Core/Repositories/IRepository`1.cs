using System.Collections.Generic;
using System.Threading.Tasks;
using CostAccounting.Core.Entities;
using CostAccounting.Core.Models;

namespace CostAccounting.Core.Repositories
{
    public interface IRepository<TEntity, in TKey> : IRepository where TEntity : Entity
    {
        Task<List<Category>> GetAsync(RequestModel request);

        Task<bool> CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(TKey id);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TKey id);
    }
}
