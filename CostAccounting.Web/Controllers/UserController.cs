using System;
using CostAccounting.Core.Models.Core;
using CostAccounting.Core.Models.Membership;
using CostAccounting.Services.Interfaces.Core;
using CostAccounting.Services.Membership;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;

        public UserController(IUserService userService, IExpenseService expenseService)
        {
            _userService = userService;
            _expenseService = expenseService;
        }

        [HttpGet("")]
        public IActionResult Get([FromQuery] UserRequestModel request)
        {
            var users = _userService.Get(request);
            return new ObjectResult(users);
        }

        [HttpGet("{id:guid}/expenses")]
        public IActionResult GetExpenses([FromRoute] Guid id, [FromQuery] ExpenseRequestModel request)
        {
            request.UserId = id;
            var users = _expenseService.Get(request);
            return new ObjectResult(users);
        }

        [AllowAnonymous]
        [HttpGet("{username}/exists")]
        public IActionResult IsUserExists([FromRoute] string username)
        {
            var userExists = _userService.GetByUsername(username);
            var isExists = userExists != null;

            return new ObjectResult(new {IsExists = isExists});
        }
    }
}