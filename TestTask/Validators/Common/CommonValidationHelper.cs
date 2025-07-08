using System.Text.RegularExpressions;

namespace TestTask.Validators.Common;

public static class CommonValidationHelper
{
    public static bool BeValidEmail(string email) => new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").IsMatch(email);
}