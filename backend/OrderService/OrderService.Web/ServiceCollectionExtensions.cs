using OrderService.Application;
using OrderService.Infrastructure;
using Serilog;

namespace OrderService.Web;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddSwaggerGen();

        services
            .AddApplication()
            .AddInfrastructure(configuration);

        services.AddSerilog();

        return services;
    }
}