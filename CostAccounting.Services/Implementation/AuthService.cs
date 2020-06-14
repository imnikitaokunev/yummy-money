using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CostAccounting.Services.Interfaces;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Models.Auth;
using CostAccounting.Services.Models.User;
using CostAccounting.Services.Settings;
using CostAccounting.Shared;
using CostAccounting.Shared.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace CostAccounting.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly SecuritySettings _securitySettings;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserService userService, SecuritySettings securitySettings, JwtSettings jwtSettings)
        {
            _userService = userService;
            _securitySettings = securitySettings;
            _jwtSettings = jwtSettings;
        }

        public AuthenticationResult Register(UserRegistrationModel model)
        {
            // TODO: May be special RegisterModel can be used here?

            Expect.ArgumentNotNull(model, nameof(model));

            // TODO: Check email and username

            var existingUser = _userService.GetByUsername(model.Username);

            // TODO: Discovery: May be such result can be added as controllers response?

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this login already exists."}
                };
            }

            // TODO: Можно вынести pass, email and login в одну таблицу, а остальное в другую
            // и тогда можно будет при регистрации указать только часть информации.

            var salt = PasswordHelper.GenerateSalt(_securitySettings.SaltLength);
            var hash = PasswordHelper.ComputeHash(model.Password, salt);

            // TODO: Mapster?

            var newUser = new UserModel
            {
                Id = SqlServerFriendlyGuid.Generate(),
                Email = model.Email,
                Username = model.Username,
                PasswordSalt = salt,
                PasswordHash = hash,
                FirstName = "Registered",
                LastName = "Registered",
                RegisteredAt = DateTime.Now,
                Photo = null
            };

            _userService.CreateUser(newUser);

            return GenerateAuthenticationResultForUser(newUser);
        }

        public AuthenticationResult Login(UserLoginModel model)
        {
            Expect.ArgumentNotNull(model, nameof(model));

            var user = _userService.GetByUsername(model.Username);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    // TODO: Or user does not exists?
                    Errors = new[] {"User with that username does not exists."}
                };
            }

            var userHasValidPassword = _userService.VerifyPassword(user, model.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"Invalid password."}
                };
            }

            return GenerateAuthenticationResultForUser(user);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(UserModel user)
        {
            Expect.ArgumentNotNull(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
                }),
                Expires = DateTime.Now.AddSeconds(_jwtSettings.TokenLifetimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new AuthenticationResult
            {
                Success = true,
                Token = token
            };
        }
    }
}