using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.UsersDtos;
using SchoolSystem.Domain.Common;
using SchoolSystem.Domain.Models;

namespace SchoolSystemApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]

    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("users-without-role")]
        public async Task<IActionResult> GetUsersWithoutRole()
        {
            var users = _userManager.Users.ToList();
            var usersWithoutRoles = new List<object>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Any())
                {
                    usersWithoutRoles.Add(new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.FullName
                    });
                }
            }
            return Ok(usersWithoutRoles);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found.");

            var result = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Role assigned successfully.");
        }
    }

}
