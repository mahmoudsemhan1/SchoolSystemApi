using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.UsersDtos;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Models;

namespace SchoolSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }
       

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserNameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            }

            if (user == null)
                return Unauthorized("Invalid username or password.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
                return Unauthorized("Invalid username or password.");

            var token = await _tokenService.GenerateTokenAsync(user);

            return Ok(new { Token = token });

        }
    }
}
