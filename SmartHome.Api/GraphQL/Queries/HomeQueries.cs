using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Homes;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType("Query")]
public class HomeQueries (IMediator mediator, ILogger<HomeQueries> logger)
{
    public async Task<List<GetHomeResponse>> GetAllHome()
    {
        logger.LogInformation("Getting all homes");
        
        var homes = await mediator.Send(new GetAllHomesQuery());
        return homes.Select(HomeTypeMapper.MapFromDomain).ToList();
    }
}