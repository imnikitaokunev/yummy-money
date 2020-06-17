using CostAccounting.Services.Models.Auth;
using CostAccounting.Services.Models.Security;
using CostAccounting.Services.Models.User;

namespace CostAccounting.Services.Auth
{
    public interface IAuthService
    {
         AuthenticationResult Register(UserRegistrationModel model);
         AuthenticationResult Login(UserLoginModel model);
         AuthenticationResult Refresh(RefreshTokenModel model);
    }
}