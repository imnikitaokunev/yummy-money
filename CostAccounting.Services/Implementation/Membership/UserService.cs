using System;
using System.Collections.Generic;
using System.Linq;
using CostAccounting.Core.Models;
using CostAccounting.Core.Models.Membership;
using CostAccounting.Core.Repositories.Membership;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Mappers;
using CostAccounting.Services.Models.User;

namespace CostAccounting.Services.Implementation.Membership
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) => _repository = repository;

        public List<UserModel> Get(RequestModel requestModel)
        {
            var users = _repository.Get(new UserRequestModel{Includes = new List<string>{"Roles", "Roles.Role"}}).Select(x => x.ToModel()).ToList();
            return users;
        }

        public UserModel GetById(Guid id)
        {
            var user = _repository.GetById(id);
            return user?.ToModel();
        }
    }
}
