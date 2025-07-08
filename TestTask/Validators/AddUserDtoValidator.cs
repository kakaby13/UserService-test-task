using System.Text.RegularExpressions;
using FluentValidation;
using TestTask.Models.Dto;
using TestTask.Repositories;

namespace TestTask.Validators;

public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
    public AddUserDtoValidator(UserRoleRepository userRoleRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Invalid input");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Invalid input")
            .Must(BeValidEmail).WithMessage("Invalid email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Invalid input");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Invalid input")
            .MustAsync(async (role, _) => await userRoleRepository.IsRoleExistsAsync(role))
            .WithMessage("Role doesn't exist");
    }

    private static bool BeValidEmail(string email) => new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").IsMatch(email);
}