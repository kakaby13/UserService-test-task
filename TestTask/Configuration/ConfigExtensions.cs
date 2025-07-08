using Microsoft.EntityFrameworkCore;
using TestTask.Repositories;
using TestTask.Services;

namespace TestTask.Configuration;

public static class ConfigExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<UserService>()
            
            .AddTransient<UserRoleRepository>()
            .AddTransient<UserRepository>();       
        
        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        services.AddDbContext<AddDbContext>(options => options.UseSqlServer(connectionString));        
        
        return services;
    }
}