using Microsoft.EntityFrameworkCore;
using TestTask.Configuration;
using TestTask.Models.Entities;

namespace TestTask.Repositories;

public class UserRepository(AddDbContext context)
{
    public async Task<User> AddNewUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return user;
    }

    public async Task<bool> IsUserExistsAsync(int userId)
    {
        return await context.Users.AnyAsync(x => x.Id == userId);
    }
    
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await context.Users.SingleAsync(x => x.Id == userId);
    }

    public async Task<List<string>> GetUsersNamesAsync()
    {
        return await context.Users.Select(x => x.Name).ToListAsync();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        
        return user;
    }
}