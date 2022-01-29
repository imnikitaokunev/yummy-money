using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Identity;
using Application.Models.User;

namespace Application.Common.Interfaces.Services;

public interface IIdentityService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<AuthenticateResponse> SignInAsync(SignInRequest request);
    Task<AuthenticateResponse> SignUpAsync(SignUpRequest request);
}
