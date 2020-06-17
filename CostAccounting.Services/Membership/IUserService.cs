using System;
using System.Collections.Generic;
using System.Security.Claims;
using CostAccounting.Core.Models;
using CostAccounting.Services.Models.User;

namespace CostAccounting.Services.Interfaces.Membership
{
    public interface IUserService
    {
        List<UserModel> Get(RequestModel requestModel);
        UserModel GetById(Guid id);
        UserModel GetByUsername(string username);
        UserModel GetByEmail(string email);
        void CreateUser(UserModel userModel);
        bool VerifyPassword(UserModel user, string password);
        List<Claim> GetUserClaimsByUserId(Guid id);
    }
}