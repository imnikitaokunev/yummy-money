using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Helpers;
using Application.Common.Interfaces.Services;
using Application.Common.Security;
using Application.Models.Identity;
using Application.Models.User;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> SignInAsync(SignInRequest request)
        {
            Require.NotNull(request, nameof(request));

            var user = await _userManager.FindByEmailAsync(request.Login) ??
                       await _userManager.FindByNameAsync(request.Login);
            if (user == null)
            {
                return new AuthenticateResponse
                {
                    Succeeded = false,
                    Errors = new[]
                    {
                        new IdentityError
                        {
                            Code = "UserNotFound",
                            Description = "User not found."
                        }
                    }
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                return new AuthenticateResponse
                {
                    Succeeded = false,
                    Errors = new[]
                    {
                        new IdentityError
                        {
                            Code = "InvalidPassword",
                            Description = "Invalid password."
                        }
                    }
                };
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse
            {
                Succeeded = true,
                Token = token
            };
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await _userManager.Users.ProjectToType<UserDto>().ToListAsync();
        }

        public async Task<AuthenticateResponse> SignUpAsync(SignUpRequest request)
        {
            Require.NotNull(request);

            var applicationUser = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(applicationUser, request.Password);
            if (!result.Succeeded)
            {
                return _mapper.Map<AuthenticateResponse>(result);
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse
            {
                Succeeded = true,
                Token = token
            };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new(nameof(user.Id), user.Id)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenLifetimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
