using System.Text.Json;
using TestTask.Models.Dto;

namespace TestTask.Services.Common;

public static class JsonParserExtensions
{
    private const string IdPropertyName = "id";
    
    private const string UserNamePropertyName = "username";
    
    private const string EmailPropertyName = "email";
    
    private const string RoleNamePropertyName = "role";
    
    public static UpdateUserDto ParseToUpdateUserDto(this string json)
    {
        using var document = JsonDocument.Parse(json);

        var result = new UpdateUserDto
        {
            Id = document.RootElement.TryGetProperty(IdPropertyName, out var idProperty) &&
                 idProperty.TryGetInt32(out var id)
                ? id
                : 0,
            Name = document.RootElement.GetProperty(UserNamePropertyName).GetString() ?? string.Empty,
            Email = document.RootElement.GetProperty(EmailPropertyName).GetString() ?? string.Empty,
            RoleName = document.RootElement.GetProperty(RoleNamePropertyName).GetString() ?? string.Empty,
        };

        return result;
    }
}