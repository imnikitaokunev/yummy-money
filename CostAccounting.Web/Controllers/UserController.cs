using CostAccounting.Core.Models.Membership;
using CostAccounting.Services.Interfaces.Membership;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service) => _service = service;

        [HttpGet("")]
        public IActionResult Get([FromQuery] UserRequestModel request)
        {
            var users = _service.Get(request);
            return new ObjectResult(users);
        }
    }
}