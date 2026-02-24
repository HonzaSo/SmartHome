using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class RoomMutations (IMediator mediator, ILogger<RoomMutations> logger)
{
    public async Task<Guid> CreateRoom(RoomTypeRequest request)
    {
        logger.LogInformation("Creating room {RoomName}", request.Name);
        var domainRoomType = (RoomType)request.Type;
        
        var command = new CreateRoomCommand()
        {
            Name = request.Name,
            Type =  domainRoomType,
            HomeId = request.HomeId
        };
        
        return await mediator.Send(command);
    }

    public async Task<RoomRemovalResult> RemoveRoomById(Guid roomId)
    {
        logger.LogInformation("Removing room by id: {RoomId}", roomId);

        var request = new RemoveRoomCommand()
        {
            Id = roomId
        };

        var result = await mediator.Send(request);

        return result switch
        {
            DeleteResultStatus.Deleted => RoomRemovalResult.Success,
            DeleteResultStatus.NotFound => RoomRemovalResult.NotFound,
            DeleteResultStatus.HasRelatedRecords => RoomRemovalResult.HasRelatedRecords,
            _ => RoomRemovalResult.Failure
        };
    }
}