using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CostAccounting.Core.Entities.Security;
using CostAccounting.Services.Interfaces.Membership;
using CostAccounting.Services.Models.Auth;
using CostAccounting.Services.Models.Security;
using CostAccounting.Services.Models.User;
using CostAccounting.Services.Security;
using CostAccounting.Services.Settings;
using CostAccounting.Shared;
using CostAccounting.Shared.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace CostAccounting.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly SecuritySettings _securitySettings;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IUserService userService, SecuritySettings securitySettings, JwtSettings jwtSettings,
            TokenValidationParameters tokenValidationParameters, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _securitySettings = securitySettings;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenService = refreshTokenService;
        }

        public AuthenticationResult Register(UserRegistrationModel model)
        {
            Expect.ArgumentNotNull(model, nameof(model));

            var existingUsername = _userService.GetByUsername(model.Username);
            var existingEmail = _userService.GetByEmail(model.Username);

            // TODO: Discovery: May be such result can be added as controllers response?

            if (existingUsername != null || existingEmail != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this username or email already exist."}
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

        public AuthenticationResult Refresh(RefreshTokenModel token)
        {
            var validatedToken = GetPrincipalFromToken(token.Token);

            if (validatedToken == null)
            {
                return new AuthenticationResult {Errors = new[] {"Invalid token."}};
            }

            var expiryDateTimeUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateTimeUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult {Errors = new[] {"This token does not expired yet."}};
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = _refreshTokenService.GetRefreshToken(new Guid(token.RefreshToken));

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token does not exist."}};
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been expired."}};
            }

            if (storedRefreshToken.IsInvalidated)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been invalidated."}};
            }

            if (storedRefreshToken.IsUsed)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token has been used."}};
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult {Errors = new[] {"This refresh token does not match this JWT."}};
            }

            storedRefreshToken.IsUsed = true;
            _refreshTokenService.Update(storedRefreshToken);

            var user = _userService.GetById(new Guid(validatedToken.Claims.Single(x => x.Type == "id").Value));
            return GenerateAuthenticationResultForUser(user);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(UserModel user)
        {
            Expect.ArgumentNotNull(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("id", user.Id.ToString())
            };

            var userClaims = _userService.GetUserClaimsByUserId(user.Id);

            claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenLifetimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            var refreshToken = new RefreshToken
            {
                Id = SqlServerFriendlyGuid.Generate(),
                JwtId = securityToken.Id,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenLifetimeInSeconds)
            };

            _refreshTokenService.Create(refreshToken);

            return new AuthenticationResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken.Id
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) =>
            validatedToken is JwtSecurityToken jwtSecurityToken &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                StringComparison.InvariantCultureIgnoreCase);
    }
}