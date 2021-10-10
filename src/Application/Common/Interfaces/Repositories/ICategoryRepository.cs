using Domain.Entities;
using System;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
