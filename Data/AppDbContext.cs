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
    }

}
