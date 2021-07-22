using System;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
