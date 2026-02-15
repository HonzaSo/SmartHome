using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Homes;
using SmartHomeApi.GraphQL.Mutations;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

public class HomeQueries (IMediator mediator, ILogger<HomeMutations> logger)
{
    public async Task<List<GetHomeResponse>> GetAllHome()
    {
        logger.LogInformation("Getting all homes");
        
        var homes = await mediator.Send(new GetAllHomesQuery());
        return homes.Select(HomeTypeMapper.MapFromDomain).ToList();
    }
}