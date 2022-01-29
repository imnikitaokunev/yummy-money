using System;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
}
