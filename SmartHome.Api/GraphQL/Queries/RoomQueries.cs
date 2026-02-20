using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Errors;
using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType("Query")]
public class RoomQueries(IMediator mediator, ILogger<RoomQueries> logger)
{
    public async Task<IGetRoomsResult> GetRoomsByHomeId(Guid homeId)
    {
        logger.LogInformation("Getting rooms for homeId {HomeId}", homeId);
        
        var rooms = await mediator.Send(new GetRoomsByHomeIdQuery { Id = homeId });

        if (rooms == null)
        {
            return new GetErrorResult("Home not found.", ErrorCategory.NotFound, "Home");
        }
        
        return new RoomsListResponse 
        { 
            Rooms = rooms.Select(RoomTypeMapper.MapFromDomain).ToList() 
        };
    }

    public async Task<IGetRoomResult> GetRoomById(Guid id)
    {
        logger.LogInformation("Getting room for roomId {RoomId}", id);
        
        var room = await mediator.Send(new GetRoomByIdQuery { Id = id });

        if (room == null)
        {
            return new GetErrorResult("Room not found.", ErrorCategory.NotFound, "Room");
        }
        
        return RoomTypeMapper.MapFromDomain(room);
    }
}