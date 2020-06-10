using CostAccounting.Services.Interfaces.Membership;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Web.Controllers
{
    [ApiController]
    [Route("api/roles")]
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
