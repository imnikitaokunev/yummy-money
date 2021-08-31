using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Models.Role;
using Application.Models.User;

namespace Application.Common.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAsync(GetUserRequest request);
        Task<PaginatedList<UserDto>> GetPagedResponseAsync(GetUserWithPaginationRequest request);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> AddAsync(CreateUserRequest request);
        Task UpdateAsync(UserDto user);
        Task DeleteAsync(Guid id);
        Task<List<RoleDto>> GetUserRolesAsync(Guid userId);
        Task<RoleDto> GetUserRoleByIdAsync(Guid userId, int roleId);
        Task<RoleDto> AddRoleAsync(Guid userId, CreateRoleRequest request);
    }
}
