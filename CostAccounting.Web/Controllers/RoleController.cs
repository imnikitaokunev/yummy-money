using CostAccounting.Services.Membership;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [ApiController]
    [Route("api/roles")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service) => _service = service;

        [HttpGet("")]
        public IActionResult Get()
        {
            var roles = _service.Get(null);
            return new ObjectResult(roles);
        }
    }
}
