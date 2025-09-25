using IssueTracker.API.Models;

namespace IssueTracker.API.Interfaces
{
    public interface IProjectService
    {
        Task<List<Project>> GetProjectsAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task<Project?> UpdateAsync(int id, Project project);
        Task<bool> DeleteAsync(int id);
    }
}
