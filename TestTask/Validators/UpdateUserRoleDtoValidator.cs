using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Repositories;

namespace TestTask.Validators;

public class UpdateUserRoleDtoValidator : AbstractValidator<UpdateUserRoleDto>
{
    public UpdateUserRoleDtoValidator(UserRoleRepository userRoleRepository, UserRepository userRepository)
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Invalid input")
            .MustAsync(async (role, _) => await userRoleRepository.IsRoleExistsAsync(role))
            .WithMessage("Role doesn't exist");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Invalid input")
            .MustAsync(async (userId, _) => await userRepository.IsUserExistsAsync(userId))
            .WithMessage("User doesn't exist");
    }
}