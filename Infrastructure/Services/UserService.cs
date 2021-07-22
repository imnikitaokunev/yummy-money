using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Models;
using Application.Models;
using Domain.Entities;
using MapsterMapper;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAsync(GetUserRequest request)
        {
            var users = await _userRepository.GetAsync(request);
            return _mapper.Map<List<UserDto>>(users);
        }

        public Task<PaginatedList<UserDto>> GetPagedResponseAsync(GetUserWithPaginationRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> AddAsync(CreateUserRequest request)
        {
            var entity = _mapper.Map<User>(request);

            // Use security settings.
            var salt = PasswordHelper.GenerateSalt(16);
            var hash = PasswordHelper.ComputeHash(request.Password, salt);

            entity.PasswordHash = hash;
            entity.PasswordSalt = salt;

            var created = await _userRepository.AddAsync(entity);
            return _mapper.Map<UserDto>(created);
        }

        public async Task UpdateAsync(UserDto user)
        {
            var entity = _mapper.Map<User>(user);
            await _userRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<List<RoleDto>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var roles = user.Roles.ToList();
            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetUserRoleByIdAsync(Guid userId, int roleId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            var role = user.Roles.FirstOrDefault(x => x.Id == roleId);
            if (role == null)
            {
                throw new NotFoundException(nameof(Role), roleId);
            }

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> AddRoleAsync(Guid userId, CreateRoleRequest request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            var roles = await _roleRepository.GetAsync(_mapper.Map<GetRoleRequest>(request));
            var entity = roles.FirstOrDefault() ?? await _roleRepository.AddAsync(_mapper.Map<Role>(request));

            entity.Users.Add(user);
            user.Roles.Add(entity);
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<RoleDto>(entity);
        }
    }
}
