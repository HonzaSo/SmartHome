using SmartHomeApi.GraphQL.Mutations;
using SmartHomeApi.GraphQL.Queries;

namespace SmartHomeApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDi(this IServiceCollection services)
    {
        services.AddScoped<HomeMutations>();
        services.AddScoped<HomeQueries>();

        return services;
    }
}