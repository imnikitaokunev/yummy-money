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

            // TODO: Mapster?

            // TODO: Можно вынести pass, email and login в одну таблицу, а остальное в другую
            // и тогда можно будет при регистрации указать только часть информации.

            var salt = PasswordHelper.GenerateSalt(_securitySettings.SaltLength);
            var hash = PasswordHelper.ComputeHash(model.Password, salt);

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
                Photo = null,
            };

            _userService.CreateUser(newUser);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, newUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, newUser.Username),
                }),
                Expires = DateTime.Now.AddSeconds(_jwtSettings.TokenLifetimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
