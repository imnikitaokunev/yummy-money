using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Models.Category;
using Application.Models.Role;

namespace Application.Common.Interfaces.Services
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAsync(GetRoleRequest request);
        Task<PaginatedList<RoleDto>> GetPagedResponseAsync(GetCategoryWithPaginationRequest request);
        Task<RoleDto> GetByIdAsync(int id);
        Task<RoleDto> AddAsync(CreateRoleRequest category);
        Task UpdateAsync(RoleDto category);
        Task DeleteAsync(int id);
    }
}
