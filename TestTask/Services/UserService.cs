using AutoMapper;
using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Models.Entities;
using TestTask.Repositories;

namespace TestTask.Services;

public class UserService(
    UserRepository userRepository, 
    UserRoleRepository userRoleRepository, 
    IMapper mapper,
    IValidator<UpdateUserRoleDto> updateUserRoleValidator,
    IValidator<AddUserDto> userValidator)
{
    public async Task CreateUser(AddUserDto addUserDto)
    {
        // todo use validation result
        var validationResult = await userValidator.ValidateAsync(addUserDto);
        
        var user = mapper.Map<User>(addUserDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password);
        user.UserRole = await userRoleRepository.GetRoleByNameAsync(addUserDto.Role);
        
        await userRepository.AddNewUserAsync(user);
    }

    public async Task<List<string>> GetUsers()
    {
        var users = await userRepository.GetUsersNamesAsync();

        return users;
    }

    public async Task UpdateUserRole(UpdateUserRoleDto updateUserRoleDto)
    {
        // todo use validation result
        var validationResult = await updateUserRoleValidator.ValidateAsync(updateUserRoleDto);
        
        var userRole = await userRoleRepository.GetRoleByNameAsync(updateUserRoleDto.RoleName);
        var user = await userRepository.GetUserByIdAsync(updateUserRoleDto.UserId);
        user.UserRole = userRole;
        await userRepository.UpdateUserAsync(user);
    }
}