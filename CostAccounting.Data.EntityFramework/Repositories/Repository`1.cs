using System;
using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Entities;
using CostAccounting.Core.Exceptions;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories;
using CostAccounting.Data.EntityFramework.Extensions;
using CostAccounting.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CostAccounting.Data.EntityFramework.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity
    {
        protected readonly CostAccountingContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(CostAccountingContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public List<TEntity> Get(RequestModel request)
        {
            try
            {
                return ApplyFilter(request).ToList();
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public void Create(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, nameof(entity));

            try
            {
                DbSet.Add(entity);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public TEntity GetById(TKey id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public void Update(TEntity entity)
        {
            Expect.ArgumentNotNull(entity, nameof(entity));

            try
            {
                DbSet.Update(entity);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public void Delete(TEntity entity)
        {
            Expect.ArgumentNotNull(entity);

            try
            {
                DbSet.Remove(entity);
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (SqlException ex)
            {
                throw ex.ToRepositoryException();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex);
            }
        }
        
        private IQueryable<TEntity> ApplyFilter(RequestModel request)
        {
            var query = DbSet.AsQueryable();
            
            IncludeInternal(query, request);
            
            return ApplyFilterInternal(query, request);
        }

        protected abstract IQueryable<TEntity> ApplyFilterInternal(IQueryable<TEntity> query, RequestModel requestModel);

        protected virtual IQueryable<TEntity> IncludeInternal(IQueryable<TEntity> query, RequestModel request)
        {
            if (request?.Includes?.Count == 0)
            {
                return query;
            }

            return request.Includes.Aggregate(query, (current, include) => current.Include(include));
        }
    }
}