using System;
using System.Collections.Generic;
using CostAccounting.Core.Models;
using CostAccounting.Services.Models.User;

namespace CostAccounting.Services.Interfaces.Membership
{
    public interface IUserService
    {
        List<UserModel> Get(RequestModel requestModel);
        UserModel GetById(Guid id);
        UserModel GetByUsername(string username);
        void CreateUser(UserModel userModel);
        bool VerifyPassword(UserModel user, string password);
    }
}