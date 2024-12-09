using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Core.Entities.User> Users { get; set; }

        // Optional: Fluent API or configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data, configurations, etc.
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "user1", Password = "1234" },
                new User { Id = 2, Username = "user2", Password = "1234" }
            );
        }
    }
}
