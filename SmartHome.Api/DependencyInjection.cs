using SmartHomeApi.GraphQL.Mutations;

namespace SmartHomeApi;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDi(this IServiceCollection services)
    {
        services.AddScoped<HomeMutations>();

        return services;
    }
}