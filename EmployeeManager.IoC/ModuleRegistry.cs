using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using EmployeeManager.Infra.Repositories;
using EmployeeManager.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeManager.IoC;

public static class ModuleRegistry
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        
        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Database;
        });
        
        services.AddScoped<IGridFsService, GridFsService>();
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        
        return services;
    }

    public static IServiceCollection AddApplicationModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<GridFsService>();
        
        return services;
    }
}