using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Application.DTOs.UsersDtos;
using SchoolSystem.Application.Interfaces;
using SchoolSystem.Domain.Models;
using System.Text.RegularExpressions;

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
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // ✅ 1. التحقق من الفورمات
            if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("Invalid email format.");

            if (!Regex.IsMatch(dto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                return BadRequest("Password must be at least 8 characters long and contain upper, lower case letters, and numbers.");

            var existingUser = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
                return BadRequest("Username is already taken.");

            var existingEmail = await _userManager.FindByEmailAsync(dto.Email);
            if (existingEmail != null)
                return BadRequest("Email is already registered.");

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                FullName = dto.FullName,
                Gender = dto.Gender,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // ✅ 2. إشعار الأدمن (محاكاة إرسال إيميل)
            // تقدر تبدلها بـ IEmailService.SendAsync(...)

            Console.WriteLine($"[NotifyAdmin] User {dto.UserName} has registered and is awaiting role approval.");

            return Ok("Registration successful. Please wait for admin to assign your role.");
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
                return Unauthorized("Invalid username or email .");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
                return Unauthorized("Invalid username or password.");

            // 🔴 التحقق من وجود Roles للمستخدم
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
                return Unauthorized("Your account is pending admin approval. No role assigned yet.");

            var token = await _tokenService.GenerateTokenAsync(user);

            return Ok(new { Token = token });

        }
    }
}
