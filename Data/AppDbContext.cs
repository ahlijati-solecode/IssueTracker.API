using IssueTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IssueTracker.API.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor calling the Base DbContext Class Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder
                            optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Category> Categories { get; set; } // Table "Categories"
        public DbSet<Project> Projects { get; set; } // Table "Projects"
        public DbSet<Issue> Issues { get; set; } // Table "Issues"
        public DbSet<Comment> Comments { get; set; } // Table "Comments"
    }

}
