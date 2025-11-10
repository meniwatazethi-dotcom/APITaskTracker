using APITaskTracker.Data.Enums;
using APITaskTracker.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace APITaskTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasData(
               new TaskItem
               {
                   Id = 1,
                   Title = "Design Classes",
                   Description = "Define classes, their attributes, and methods to represent objects in the system",
                   Status = TaskItemStatus.Done,
                   Priority = TaskPriority.High,
                   CreatedAt = DateTime.UtcNow.AddDays(-1),
                   DueDate = DateTime.UtcNow.AddDays(1),
               },
                new TaskItem
                {
                    Id = 2,
                    Title = "Implement Inheritance",
                    Description = "Create derived classes from a base class to reuse code and model hierarchical relationships",
                    Status = TaskItemStatus.InProgress,
                    Priority = TaskPriority.High,
                    DueDate = DateTime.UtcNow.AddDays(2),
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new TaskItem
                {
                    Id = 3,
                    Title = "Implement Polymorphism",
                    Description = "Use method overriding or interfaces so that objects of different classes can be treated uniformly while behaving differently",
                    Status = TaskItemStatus.New,
                    Priority = TaskPriority.Medium,
                    DueDate = DateTime.UtcNow.AddDays(5),
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            );
        }
    }
}
