using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Models.Common;
using Domain.Common;

namespace Application.Common.Interfaces;

public interface IRepository<TEntity, in TKey> where TEntity : Entity<TKey>
{
    Task<TEntity> GetByIdAsync(TKey id);
    Task<List<TEntity>> GetAsync(Request request);
    Task<PaginatedList<TEntity>> GetPagedResponseAsync(PaginationRequest request);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = new());
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = new());
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = new());
}