using IssueTracker.API.Data;
using IssueTracker.API.Interfaces;
using IssueTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly AppDbContext _context;
        public IssueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Issue> CreateAsync(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return false;
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Issue?> GetIssueByIdAsync(int id)
        {
            return await _context.Issues.FindAsync(id);
        }

        public async Task<List<Issue>> GetIssuesAsync()
        {
            return await _context.Issues.Include(dm => dm.Project).ToListAsync();
        }

        public async Task<Issue?> UpdateAsync(int id, Issue issue)
        {
            var existing = await _context.Issues.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            existing.Title = issue.Title;
            existing.Description = issue.Description;
            existing.Status = issue.Status;
            existing.Priority = issue.Priority;
            existing.ProjectId = issue.ProjectId;
            existing.AssignedToUserId = issue.AssignedToUserId;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
