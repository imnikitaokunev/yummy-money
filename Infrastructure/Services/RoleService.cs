using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.Models;
using Domain.Entities;
using MapsterMapper;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> GetAsync(GetRoleRequest request)
        {
            var roles = await _roleRepository.GetAsync(request);
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public Task<PaginatedList<RoleDto>> GetPagedResponseAsync(GetCategoryWithPaginationRequest request) => throw new NotImplementedException();

        public Task<RoleDto> GetByIdAsync(int id) => throw new NotImplementedException();

        public async Task<RoleDto> AddAsync(CreateRoleRequest category)
        {
            var entity = _mapper.Map<Role>(category);
            var created = await _roleRepository.AddAsync(entity);
            return _mapper.Map<RoleDto>(created);
        }

        public Task UpdateAsync(RoleDto category) => throw new NotImplementedException();

        public Task DeleteAsync(int id) => throw new NotImplementedException();
    }
}
