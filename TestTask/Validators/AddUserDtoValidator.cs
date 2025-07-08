using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Repositories.Interfaces;
using TestTask.Validators.Common;

namespace TestTask.Validators;

public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
    public AddUserDtoValidator(IUserRoleRepository userRoleRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Invalid input");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Invalid input")
            .Must(CommonValidationHelper.BeValidEmail).WithMessage("Invalid email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Invalid input");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Invalid input")
            .MustAsync(async (role, _) => await userRoleRepository.IsRoleExistsAsync(role))
            .WithMessage("Role doesn't exist");
    }
}