using TestTask.Models;

namespace TestTask.Repositories;

public class UserRepository
{
    public Task<User> AddNewUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetUsersNamesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}