using Application.Services.UserService;
using Application.Services.UserService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto input)
        {
            await _userService.CreateUser(input);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id , [FromBody] UpdateUserDto input)
        {
            await _userService.UpdateUser(id, input);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(String? name , string? email)
        {
            var users = await _userService.GetAllUsers(name, email);
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById (Guid id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }


    }
}
