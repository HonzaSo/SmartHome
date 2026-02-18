using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.Mappers;
using GetErrorResult = SmartHomeApi.GraphQL.Interfaces.GetErrorResult;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType("Query")]
public class RoomQueries(IMediator mediator, ILogger<RoomQueries> logger)
{
    public async Task<List<RoomTypeResponse>> GetRoomsByHomeId(Guid homeId)
    {
        logger.LogInformation("Getting rooms for homeId {HomeId}", homeId);
        
        var rooms = await mediator.Send(new GetRoomsByHomeIdQuery {Id =  homeId});
        return rooms.Select(RoomTypeMapper.MapFromDomain).ToList();
    }

    public async Task<IGetRoomResult> GetRoomById(Guid id)
    {
        logger.LogInformation("Getting room for roomId {RoomId}", id);
        
        var room = await mediator.Send(new GetRoomByIdQuery { Id = id });

        if (room == null)
        {
            return new GetErrorResult(id.ToString(), ErrorCategory.NotFound, "Room not found.");
        }
        
        return RoomTypeMapper.MapFromDomain(room);
    }
}