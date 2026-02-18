using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Homes;
using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType("Query")]
public class HomeQueries (IMediator mediator, ILogger<HomeQueries> logger)
{
    public async Task<IGetHomeResult> GetHomeById(Guid id)
    {
        logger.LogInformation("Getting home by id {HomeId}", id);
        
        var home = await mediator.Send(new GetHomeByIdQuery { Id = id });

        if (home == null)
        {
            return new GetErrorResult(id.ToString(), ErrorCategory.NotFound, "Home not found.");
        }
        
        return HomeTypeMapper.MapFromDomain(home);
    }
    
    public async Task<List<HomeTypeResponse>> GetAllHome()
    {
        logger.LogInformation("Getting all homes");
        
        var homes = await mediator.Send(new GetAllHomesQuery());
        return homes.Select(HomeTypeMapper.MapFromDomain).ToList();
    }
}