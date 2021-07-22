using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository: Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected override DbSet<User> DbSet => Context.Users;

        public override async Task<User> GetByIdAsync(Guid id) =>
            await DbSet.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

        protected override IQueryable<User> ApplyFilterInternal(IQueryable<User> query, Request request)
        {
            if (request is not GetUserRequest getUserRequest)
            {
                return query;
            }

            return query.Include(x => x.Roles);
        }
    }
}
