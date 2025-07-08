using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Repositories.Interfaces;

namespace TestTask.Validators;

public class UpdateUserRoleDtoValidator : AbstractValidator<UpdateUserRoleDto>
{
    public UpdateUserRoleDtoValidator(IUserRoleRepository userRoleRepository, IUserRepository userRepository)
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