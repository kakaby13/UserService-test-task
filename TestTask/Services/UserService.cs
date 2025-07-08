using TestTask.Models;
using TestTask.Repositories;

namespace TestTask.Services;

public class UserService(UserRepository userRepository, UserRoleRepository userRoleRepository)
{
    public async Task CreateUser(string name, string email, string password, string role)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            throw new Exception("Invalid input");
        }

        var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email))
        {
            throw new Exception("Invalid email");
        }
        
        var user = new User
        {
            Name = name,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            UserRole = await userRoleRepository.GetRoleByNameAsync(role)
        };
        
        await userRepository.AddNewUserAsync(user);
    }

    public async Task<List<string>> GetUsers()
    {
        var users = await userRepository.GetUsersNamesAsync();

        return users;
    }

    public async Task UpdateUserRole(int userId, string newRole)
    {
        if (newRole != "Admin" && newRole != "User")
        {
            throw new Exception("Invalid role");
        }
        
        var userRole = await userRoleRepository.GetRoleByNameAsync(newRole);
        var user = await userRepository.GetUserByIdAsync(userId);
        user.UserRole = userRole;
        await userRepository.UpdateUserAsync(user);
    }
}