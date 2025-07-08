using TestTask.Models.Entities;

namespace TestTask.Repositories.Interfaces;

public interface IUserRoleRepository
{
    public Task<UserRole> GetRoleByNameAsync(string name);

    public Task<bool> IsRoleExistsAsync(string name);
}