using TestTask.Models.Entities;

namespace TestTask.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User> AddNewUserAsync(User user);

    public Task<bool> IsUserExistsAsync(int userId);

    public Task<User> GetUserByIdAsync(int userId);

    public Task<List<string>> GetUsersNamesAsync();

    public Task<User> UpdateUserAsync(User user);
}