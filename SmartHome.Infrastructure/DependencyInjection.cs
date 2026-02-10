using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Application.Interfaces;
using SmartHome.Infrastructure.Configurations;
using SmartHome.Infrastructure.Repositories;

namespace SmartHome.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDi(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSection = configuration.GetSection("Database");
    
        services.Configure<Database>(databaseSection); 

        var dbSettings = databaseSection.Get<Database>(); 

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(dbSettings?.ConnectionString, x => 
                x.MigrationsAssembly("SmartHome.Infrastructure"));
        });

        services.AddScoped<IHomeRepository, HomeRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();

        return services;
    }
}