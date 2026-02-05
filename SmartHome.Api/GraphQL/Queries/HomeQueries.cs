using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Mutations;
using SmartHomeApi.GraphQL.Types;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

public class HomeQueries (IMediator mediator, ILogger<HomeMutations> logger)
{
    public async Task<List<GetHomeType>> GetAllHome()
    {
        logger.LogInformation("Getting all homes");
        
        var homes = await mediator.Send(new GetAllHomesQuery());
        return homes.Select(HomeTypeMapper.MapFromDomain).ToList();
    }
}