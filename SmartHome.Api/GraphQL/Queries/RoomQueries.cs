using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType(typeof(Query))]
public class RoomQueries(IMediator mediator, ILogger<RoomQueries> logger)
{
    public async Task<List<RoomTypeResponse>> GetRoomsByHomeId(Guid homeId)
    {
        logger.LogInformation("Getting rooms for homeId {HomeId}", homeId);
        
        var rooms = await mediator.Send(new GetRoomsByHomeIdQuery {Id =  homeId});
        return rooms.Select(RoomTypeMapper.MapFromDomain).ToList();
    }
}