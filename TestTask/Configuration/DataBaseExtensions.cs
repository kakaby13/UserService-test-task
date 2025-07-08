using TestTask.Models.Entities;

namespace TestTask.Configuration;

public static class DataBaseExtensions
{
    public static async Task SetupDefaultData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();

        if (!context.UserRoles.Any())
        {
            context.UserRoles.AddRange(new UserRole
                {
                    Name = "Admin",
                },
                new UserRole
                {
                    Name = "User",
                });
            
            await context.SaveChangesAsync();
        }
        
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Name = "Foo", Email = "foo@mail.com", PasswordHash = "passwordHash", UserRoleId = 1},
                new User { Name = "bar", Email = "bar@example.com", PasswordHash = "passwordHash", UserRoleId = 2}
            );

            await context.SaveChangesAsync();
        }
    }
}