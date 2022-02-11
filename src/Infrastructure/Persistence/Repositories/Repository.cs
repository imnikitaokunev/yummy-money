using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Extensions;
using Application.Models.Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        protected readonly IApplicationDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(IApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAsync(Request request)
        {
            return await Include(ApplyFilter(DbSet, request)).AsNoTracking().ToListAsync();
        }

        public async Task<PaginatedList<TEntity>> GetPagedResponseAsync(PaginationRequest request)
        {
            return await Include(ApplyFilter(DbSet, request)).AsNoTracking().PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = new())
        {
            var created = await DbSet.AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return created.Entity;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = new())
        {
            var isExists = await DbSet.AnyAsync(x => x.Id.Equals(entity.Id), cancellationToken);
            if (!isExists)
            {
                throw new NotFoundException(nameof(TEntity), entity.Id);
            }

            DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TKey id, CancellationToken cancellationToken = new())
        {
            var existing = await DbSet.FindAsync(new object[] { id }, cancellationToken);
            if (existing == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, id);
            }

            DbSet.Remove(existing);
            await Context.SaveChangesAsync(cancellationToken);
        }

        protected abstract IQueryable<TEntity> ApplyFilterInternal(IQueryable<TEntity> query, Request request);

        protected virtual IQueryable<TEntity> Include(IQueryable<TEntity> query)
        {
            return query;
        }

        protected IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, Request request)
        {
            return ApplyFilterInternal(query, request);
        }
    }
}
