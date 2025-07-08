using Microsoft.EntityFrameworkCore;
using TestTask.Configuration;
using TestTask.Models.Entities;
using TestTask.Repositories.Interfaces;

namespace TestTask.Repositories.Implementations;

public class UserRoleRepository(AppDbContext context) : IUserRoleRepository
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