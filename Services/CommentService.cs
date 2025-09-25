using IssueTracker.API.Data;
using IssueTracker.API.Interface;
using IssueTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var existing = await _context.Comments.FindAsync(Id);
            if (existing == null)
            {
                return false;
            }
            _context.Comments.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int Id)
        {
            return await _context.Comments.FindAsync(Id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var existing = await _context.Comments.FindAsync(id);
            if (existing == null)
            {
                return null;
            }
            existing.Content = comment.Content;
            await _context.SaveChangesAsync();

            return existing;
        }
    }
}
