using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Action", DisplayOrder = 1 },
            new { Id = 2, Name = "SciFi", DisplayOrder = 2 },
            new { Id = 3, Name = "History", DisplayOrder = 3 }
            );

        base.OnModelCreating(modelBuilder);
    }
}
