using IssueTracker.API.Interfaces;
using IssueTracker.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IssueTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase

    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {   
            var userList = await _userService.GetAllUsersAsync(); 
            return Ok(userList);
        }

        // GET: /api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) 
                return NotFound($"User with ID = {id} not found.");

            return Ok(user);
        }

        // GET: /api/users/name/{username}
        [HttpGet("name/{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _userService.GetUserByUserNameAsync(userName);
            if (user == null)
                return NotFound($"User with username = {userName} not found.");

            return Ok(user);
        }

        // POST: /api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
           
            var result = await _userService.CreateUserAsync(user);
            if (result == null)
                return BadRequest();
          
            return CreatedAtAction(nameof(GetUserByUserName), new { userName = user.UserName },
                 $"New user '{user.UserName}' created successfully");
        }

        // PUT: /api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _userService.UpdateUserAsync(id, user);
            if (existing == null)
                return NotFound($"User with ID = {id} not found.");

            return Ok($"User with ID = {id} updated successfully");
        }

        // DELETE: /api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user == false)
                return NotFound($"User with ID = {id} not found.");

            return Ok($"User with ID = {id} deleted succcessfully.");
        }
    }


}

