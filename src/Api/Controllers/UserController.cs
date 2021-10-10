//using System;
//using System.Threading.Tasks;
//using Application.Common.Interfaces.Services;
//using Application.Models.Role;
//using Application.Models.User;
//using Microsoft.AspNetCore.Mvc;

//namespace Api.Controllers
//{
//    [ApiController]
//    [Route("api/users")]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;

//        public UserController(IUserService userService) => _userService = userService;

//        [HttpGet]
//        public async Task<IActionResult> GetAsync([FromQuery] GetUserRequest request)
//        {
//            var users = await _userService.GetAsync(request);
//            return Ok(users);
//        }

//        [HttpGet("{id:guid}")]
//        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
//        {
//            var user = await _userService.GetByIdAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return Ok(user);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddAsync([FromBody] CreateUserRequest request)
//        {
//            var created = await _userService.AddAsync(request);
//            return CreatedAtAction("GetById", new {id = created.Id}, created);
//        }

//        [HttpPut("{id:guid}")]
//        public async Task<IActionResult> UpdateAsync([FromBody] UserDto user, [FromRoute] Guid id)
//        {
//            if (user.Id != id)
//            {
//                return BadRequest();
//            }

//            await _userService.UpdateAsync(user);
//            return NoContent();
//        }

//        [HttpDelete("{id:guid}")]
//        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
//        {
//            await _userService.DeleteAsync(id);
//            return NoContent();
//        }

//        [HttpGet("{id:guid}/roles")]
//        public async Task<IActionResult> GetRolesAsync([FromRoute] Guid id)
//        {
//            var roles = await _userService.GetUserRolesAsync(id);
//            return Ok(roles);
//        }

//        [HttpGet("{userId:guid}/roles/{roleId:int}")]
//        public async Task<IActionResult> GetUserRoleByIdAsync([FromRoute] Guid userId, [FromRoute]int roleId)
//        {
//            var role = await _userService.GetUserRoleByIdAsync(userId, roleId);
//            return Ok(role);
//        }


//        [HttpPost("{id:guid}/roles")]
//        public async Task<IActionResult> AddUserRoleAsync([FromRoute] Guid id, [FromBody] CreateRoleRequest role)
//        {
//            var created = await _userService.AddRoleAsync(id, role);
//            return CreatedAtAction("GetById", "Role", new {id = created.Id}, created);
//        }
//    }
//}