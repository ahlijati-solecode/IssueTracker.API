using IssueTracker.API.Interfaces;
using IssueTracker.API.Models;
using Microsoft.AspNetCore.Identity;

namespace IssueTracker.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            var _user = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(_user, user.Password);
            if (!result.Succeeded)            
                return null;

            // Assign role
            if (!string.IsNullOrEmpty(user.Role))
            {
                if (!await _roleManager.RoleExistsAsync(user.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(user.Role));
                }
                await _userManager.AddToRoleAsync(_user, user.Role);
            }
            return user;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return false;

            return true;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<User>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new User { 
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,                   
                    Role = roles.FirstOrDefault()
                });
            }

            return userList;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new User
            {
                Id= user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<User?> UpdateUserAsync(string id, User user)
        {
            var _user = await _userManager.FindByIdAsync(id);
            if (_user == null)
                return null;

            _user.UserName = user.UserName;
            _user.Email = user.Email;
                 

            var result = await _userManager.UpdateAsync(_user);
            if (!result.Succeeded)
                return null;

            // Update role
            if (!string.IsNullOrEmpty(user.Role))
            {
                var currentRoles = await _userManager.GetRolesAsync(_user);
                await _userManager.RemoveFromRolesAsync(_user, currentRoles);

                if (!await _roleManager.RoleExistsAsync(user.Role))
                    await _roleManager.CreateAsync(new IdentityRole(user.Role));

                await _userManager.AddToRoleAsync(_user, user.Role);
            }

            return user;
        }
    }
}
