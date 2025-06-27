using EmployeeManager.Application.Interfaces;
using EmployeeManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManager.Infra.Context;
using EmployeeManager.Infra.Interfaces;
using EmployeeManager.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmployeeManager.IoC;

public static class ModuleRegistry
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        
        services.AddSingleton<MongoDbContext>();
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        
        return services;
    }

    public static IServiceCollection AddApplicationModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        return services;
    }
}