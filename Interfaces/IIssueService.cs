using IssueTracker.API.Models;

namespace IssueTracker.API.Interfaces
{
    public interface IIssueService
    {
        Task<List<Issue>> GetIssuesAsync();
        Task<Issue?> GetIssueByIdAsync(int id);
        Task<Issue> CreateAsync(Issue issue);
        Task<Issue> UpdateAsync(int id, Issue issue);
        Task<bool> DeleteAsync(int id);
    }
}
