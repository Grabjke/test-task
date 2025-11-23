using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application;
using OrderService.Core;
using OrderService.Infrastructure.DbContexts;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositories()
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

        services.AddScoped<IOrdersReadDbContext, ReadOrderDbContext>(_ =>
            new ReadOrderDbContext(configuration.GetConnectionString(
                InfrastructureConstants.DATABASE)!));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }
}