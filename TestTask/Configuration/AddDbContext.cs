using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Configuration;

public class AddDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserRole)
            .WithMany()
            .HasForeignKey(u => u.UserRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}