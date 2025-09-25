using IssueTracker.API.Models;

namespace IssueTracker.API.Interface
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetCommentByIdAsync(int Id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int Id, Comment comment);
        Task<bool> DeleteAsync(int Id);
    }
}
