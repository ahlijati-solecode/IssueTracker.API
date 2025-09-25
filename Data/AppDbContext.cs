using IssueTracker.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IssueTracker.API.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Roles ---
            var adminRole = new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
            var managerRole = new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" };
            var developerRole = new IdentityRole { Id = "3", Name = "Developer", NormalizedName = "DEVELOPER" };

            builder.Entity<IdentityRole>().HasData(adminRole, managerRole, developerRole);

            // --- Admin User ---
            var adminUser = new IdentityUser
            {
                Id = "10",
                UserName = "admin@site.com",
                NormalizedUserName = "ADMIN@SITE.COM",
                Email = "admin@site.com",
                NormalizedEmail = "ADMIN@SITE.COM",
                EmailConfirmed = true,
                // Password: 123456
                PasswordHash = "AQAAAAIAAYagAAAAEEKmLnbKdiXLx9+FJFWrCwd092Pky8mjhEGuWdT68rzm5Uab4MeCS9+W9ttit097EA==",
                ConcurrencyStamp = "fixed-admin-concurrency",
                SecurityStamp = "fixed-admin-security"
            };

            // --- Manager User ---
            var managerUser = new IdentityUser
            {
                Id = "11",
                UserName = "manager@site.com",
                NormalizedUserName = "MANAGER@SITE.COM",
                Email = "manager@site.com",
                NormalizedEmail = "MANAGER@SITE.COM",
                EmailConfirmed = true,
                // Password: 123456
                PasswordHash = "AQAAAAIAAYagAAAAEEKmLnbKdiXLx9+FJFWrCwd092Pky8mjhEGuWdT68rzm5Uab4MeCS9+W9ttit097EA==",
                ConcurrencyStamp = "fixed-user-concurrency",
                SecurityStamp = "fixed-user-security"
            };
            // --- Developer User ---
            var devUser = new IdentityUser
            {
                Id = "12",
                UserName = "developer@site.com",
                NormalizedUserName = "DEVELOPER@SITE.COM",
                Email = "developer@site.com",
                NormalizedEmail = "DEVELOPER@SITE.COM",
                EmailConfirmed = true,
                // Password: 123456
                PasswordHash = "AQAAAAIAAYagAAAAEEKmLnbKdiXLx9+FJFWrCwd092Pky8mjhEGuWdT68rzm5Uab4MeCS9+W9ttit097EA==",
                ConcurrencyStamp = "fixed-user-concurrency",
                SecurityStamp = "fixed-user-security"
            };

            builder.Entity<IdentityUser>().HasData(adminUser, managerUser, devUser);

            // --- Assign Roles ---
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "10", RoleId = "1" },
                new IdentityUserRole<string> { UserId = "11", RoleId = "2" },
                new IdentityUserRole<string> { UserId = "12", RoleId = "3" }
            );
        }

        public DbSet<Category> Categories { get; set; } // Table "Categories"
        public DbSet<Project> Projects { get; set; } // Table "Projects"
        public DbSet<Issue> Issues { get; set; } // Table "Issues"
        public DbSet<Comment> Comments { get; set; } // Table "Comments"
    }

}
