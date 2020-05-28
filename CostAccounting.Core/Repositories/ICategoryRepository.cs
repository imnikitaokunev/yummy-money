using System;
using CostAccounting.Core.Entities;

namespace CostAccounting.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
