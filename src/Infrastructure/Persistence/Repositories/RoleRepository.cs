using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.Models.Role;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RoleRepository : Repository<Role, int>, IRoleRepository
    {
        public RoleRepository(IApplicationDbContext context) : base(context)
        {
        }

        protected override DbSet<Role> DbSet => Context.Roles;

        protected override IQueryable<Role> ApplyFilterInternal(IQueryable<Role> query, Request request)
        {
            if (request is not GetRoleRequest getRoleRequest)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(getRoleRequest.Name))
            {
                query = query.Where(x => x.Name.Contains(getRoleRequest.Name));
            }

            return query;
        }
    }
}
