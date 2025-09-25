using Azure.Core;
using IssueTracker.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;


        public AuthController(IAuthService authService, UserManager<IdentityUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password, string role)
        {
            var result = await _authService.RegisterAsync(email, password);
            if (result)
            {
                var user = await _userManager.FindByNameAsync(email);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }

                return Ok("User registered successfully");
            }
            return BadRequest("Registration failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authService.LoginAsync(email, password);
            if (!result)
                return Unauthorized(new { Message = "Invalid login attempt" });

            // Ambil user dari email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Unauthorized(new { Message = "User not found" });

            // Ambil role user
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Developer";

            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = role,
                Message = "Login successful (cookie created)"
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok("Logged out successfully");
        }

    }
}
