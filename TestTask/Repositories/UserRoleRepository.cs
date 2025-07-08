using Microsoft.EntityFrameworkCore;
using TestTask.Configuration;
using TestTask.Models.Entities;

namespace TestTask.Repositories;

public class UserRoleRepository(AddDbContext context)
{
    public async Task<UserRole> GetRoleByNameAsync(string name)
    {
        return await context.UserRoles.SingleAsync(c => c.Name == name);
    }

    public async Task<bool> IsRoleExistsAsync(string name)
    {
        return await context.UserRoles.AnyAsync(c => c.Name == name);
    }
}