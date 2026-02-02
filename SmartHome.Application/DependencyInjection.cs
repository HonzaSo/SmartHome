using Microsoft.Extensions.DependencyInjection;
using SmartHome.Application.Commands;

namespace SmartHome.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDi(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(CreateHomeCommand).Assembly);
        });

        return services;
    }
}