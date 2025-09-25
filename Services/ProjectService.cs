using IssueTracker.API.Data;
using IssueTracker.API.Interface;
using IssueTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> UpdateAsync(int id, Project project)
        {
            var existing = await _context.Projects.FindAsync(id);
            if (existing == null) return null;

            existing.Name = project.Name;
            existing.Description = project.Description;
            await _context.SaveChangesAsync();
            return existing;
        }


    }
}
