using System.Linq;
using CostAccounting.Services.Auth;
using CostAccounting.Services.Interfaces;
using CostAccounting.Services.Models.Auth;
using CostAccounting.Services.Models.Security;
using CostAccounting.Services.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationModel user)
        {
            var authResponse = _authService.Register(user);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            var authResponse = _authService.Login(user);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenModel token)
        {
            // TODO: Можно сделать в сервисы/модели папку Requests, в которой хранить реквесты.

            var authResponse = _authService.Refresh(token);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}