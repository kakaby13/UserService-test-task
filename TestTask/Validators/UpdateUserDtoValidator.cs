using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Repositories;
using TestTask.Validators.Common;

namespace TestTask.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator(UserRoleRepository userRoleRepository, UserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Invalid input")
            .MustAsync(async (userId, _) => await userRepository.IsUserExistsAsync(userId))
            .WithMessage("Role doesn't exist");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Invalid input");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Invalid input")
            .Must(CommonValidationHelper.BeValidEmail).WithMessage("Invalid email");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Invalid input")
            .MustAsync(async (role, _) => await userRoleRepository.IsRoleExistsAsync(role))
            .WithMessage("Role doesn't exist");
    }
}