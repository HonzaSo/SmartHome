using HotChocolate.Execution.Configuration;
using SmartHomeApi.GraphQL.Dtos.Homes;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.GraphQL.Mutations;
using SmartHomeApi.GraphQL.Queries;

namespace SmartHomeApi.GraphQL.Common;

public static class GraphQlExtensions
{
    public static IRequestExecutorBuilder AddGraphQlConfiguration(this IServiceCollection services)
    {
        return services.AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<HomeQueries>()
                .AddTypeExtension<RoomQueries>()
            .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<HomeMutations>()
                .AddTypeExtension<RoomMutations>()
                .AddTypeExtension<DeviceMutations>()
            .AddResultUnion<IGetHomeResult, HomeTypeResponse>("GetHomeResult")
            .AddResultUnion<IGetRoomResult, RoomTypeResponse>("GetRoomResult");
    }
    
    private static IRequestExecutorBuilder AddResultUnion<TInterface, TResponse>(
        this IRequestExecutorBuilder builder, 
        string name)
        where TInterface : class
        where TResponse : class
    {
        return builder.AddUnionType<TInterface>(d => d
            .Name(name)
            .Type<ObjectType<TResponse>>()
            .Type<ObjectType<GetErrorResult>>());
    }
}