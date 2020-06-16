﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using CostAccounting.Core.Entities.Membership;
using CostAccounting.Core.Models;
using CostAccounting.Core.Models.Membership;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Models.User;
using CostAccounting.Shared;
using CostAccounting.Shared.Helpers;
using Mapster;

namespace CostAccounting.Services.Implementation.Membership
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) => _repository = repository;

        public List<UserModel> Get(RequestModel requestModel)
        {
            requestModel.Includes = new List<string> {"Roles", "Roles.Role"};
            return _repository.Get(requestModel).Select(x => x.Adapt<UserModel>()).ToList();
        }

        public UserModel GetById(Guid id)
        {
            var user = _repository.GetById(id);
            return user?.Adapt<UserModel>();
        }

        public UserModel GetByUsername(string username)
        {
            var request = new UserRequestModel
            {
                Username = username
            };

            var user = _repository.Get(request).FirstOrDefault();

            return user?.Adapt<UserModel>();
        }

        public void CreateUser(UserModel userModel)
        {
            Expect.ArgumentNotNull(userModel);

            var user = userModel.Adapt<User>();
            _repository.Create(user);

            // TODO: Update mapster config 

            user.Roles.Add(new UserRole
            {
                RoleId = 2,
                UserId = user.Id
            });

            _repository.Save();
        }

        public bool VerifyPassword(UserModel user, string password)
        {
            Expect.ArgumentNotNull(user, nameof(user));

            return !string.IsNullOrEmpty(password) &&
                   PasswordHelper.ComputeHash(password, user.PasswordSalt) == user.PasswordHash;
        }

        public List<Claim> GetUserClaimsByUserId(Guid id)
        {
            // TODO: GetUserById не включает в себя Includes список - нужно подумать.

            var request = new UserRequestModel
            {
                Id = id,
                Includes = new[] {"Roles", "Roles.Role"}
            };

            var user = _repository.Get(request).FirstOrDefault();
            var claims = new List<Claim>();

            // TODO: Or throw ex?

            if (user == null)
            {
                return claims;
            }

            var roleClaims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Role.Name));
            claims.AddRange(roleClaims);

            return claims;
        }
    }
}
