using CostAccounting.Core.Models;
using System.Collections.Generic;
using CostAccounting.Core.Entities;

namespace CostAccounting.Core.Repositories
{
    public interface IRepository<TEntity, in TKey> : IRepository where TEntity : Entity
    {
        List<TEntity> Get(RequestModel requestModel);

        void Create(TEntity entity);

        TEntity GetById(TKey id);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Save();
    }
}
