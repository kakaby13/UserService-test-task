using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TestTask.MappingProfiles;
using TestTask.Models.Dto;
using TestTask.Repositories;
using TestTask.Services;
using TestTask.Validators;

namespace TestTask.Configuration;

public static class ConfigExtensions
{
    public static IServiceCollection AddMappingProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMappingProfile));

        return services;
    }
    
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<AddUserDto>, AddUserDtoValidator>()
            .AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>()
            .AddScoped<IValidator<UpdateUserRoleDto>, UpdateUserRoleDtoValidator>();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<UserService>();       
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<UserRoleRepository>()
            .AddTransient<UserRepository>();       
        
        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));        
        
        return services;
    }
}