using AutoMapper;
using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Models.Entities;
using TestTask.Repositories.Interfaces;

namespace TestTask.Services;

public class UserService(
    IUserRepository userRepository, 
    IUserRoleRepository userRoleRepository, 
    IMapper mapper,
    IValidator<UpdateUserRoleDto> updateUserRoleValidator,
    IValidator<UpdateUserDto> updateUserDtoValidator,
    IValidator<AddUserDto> addUserValidator)
{
    public async Task CreateUser(AddUserDto addUserDto)
    {
        // todo use validation result
        var validationResult = await addUserValidator.ValidateAsync(addUserDto);
        
        var user = mapper.Map<User>(addUserDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password);
        user.UserRole = await userRoleRepository.GetRoleByNameAsync(addUserDto.Role);
        
        await userRepository.AddNewUserAsync(user);
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto updateUserDto)
    {
        var validationResult = await updateUserDtoValidator.ValidateAsync(updateUserDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var existingEntity = await userRepository.GetUserByIdAsync(updateUserDto.Id);
        existingEntity = mapper.Map(updateUserDto, existingEntity);
        existingEntity.UserRole = await userRoleRepository.GetRoleByNameAsync(updateUserDto.RoleName);
        await userRepository.UpdateUserAsync(existingEntity);
        
        return mapper.Map<UserDto>(existingEntity);
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