using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Core;
using OrderService.Infrastructure.DbContexts;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration);
        
        return services;
    }
    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<WriteOrderDbContext>(_ => 
            new WriteOrderDbContext(configuration.GetConnectionString(
                InfrastructureConstants.DATABASE)!));
        
        return services;
    }
}