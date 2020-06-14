using CostAccounting.Services.Models.Auth;
using CostAccounting.Services.Models.User;

namespace CostAccounting.Services.Interfaces
{
    public interface IAuthService
    {
         AuthenticationResult Register(UserRegistrationModel model);
         AuthenticationResult Login(UserLoginModel model);
    }
}