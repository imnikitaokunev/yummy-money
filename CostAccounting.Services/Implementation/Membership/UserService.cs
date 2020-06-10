using System;
using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Models;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Models.User;
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
    }
}
