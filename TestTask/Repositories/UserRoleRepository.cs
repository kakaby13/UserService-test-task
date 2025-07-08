using TestTask.Models;
using TestTask.Models.Entities;

namespace TestTask.Repositories;

public class UserRoleRepository
{
    public Task<UserRole> GetRoleByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsRoleExistsAsync(string name)
    {
        throw new NotImplementedException();
    }
}