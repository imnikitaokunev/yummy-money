using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.Models.Role;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) => _roleService = roleService;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetRoleRequest request)
        {
            var roles = await _roleService.GetAsync(request);
            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateRoleRequest role)
        {
            var created = await _roleService.AddAsync(role);
            return CreatedAtAction("GetById", new {id = created.Id}, created);
        }
    }
}