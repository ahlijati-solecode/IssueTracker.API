using IssueTracker.API.Models;

namespace IssueTracker.API.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByUserNameAsync(string userName);
        Task<User?> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(string id, User user);
        Task<bool> DeleteUserAsync(string id);
    }
}
