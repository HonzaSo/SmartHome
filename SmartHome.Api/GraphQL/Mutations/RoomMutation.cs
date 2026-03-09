using MediatR;
using SmartHome.Application.Enums;
using SmartHome.Application.Operations.Homes.Queries.GetHomeById;
using SmartHome.Application.Operations.Rooms.Commands.CreateRoom;
using SmartHome.Application.Operations.Rooms.Commands.RemoveRoom;
using SmartHome.Application.Operations.Rooms.Commands.UpdateRoom;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Errors;
using SmartHomeApi.GraphQL.Interfaces;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class RoomMutations (IMediator mediator, ILogger<RoomMutations> logger)
{
    public async Task<ICreateRoomResult> CreateRoom(CreateRoomRequest request)
    {
        logger.LogInformation("Creating room {RoomName}", request.Name);
        var home = await mediator.Send(new GetHomeByIdQuery { Id = request.HomeId });

        if (home is null)
        {
            logger.LogWarning("Home with id {HomeId} not found. Cannot create room.", request.HomeId);
            return new GetErrorResult("Home not found.", ErrorCategory.EntityNotFound, "Home");
        }
        
        var domainRoomType = (RoomType)request.Type;
        
        var command = new CreateRoomCommand
        {
            Name = request.Name,
            Type =  domainRoomType,
            HomeId = request.HomeId
        };
        
        var result =  await mediator.Send(command);
        return new CreateRoomSuccess(result);
    }

    public async Task<RoomRemovalResult> RemoveRoomById(Guid roomId)
    {
        logger.LogInformation("Removing room by id: {RoomId}", roomId);

        var request = new RemoveRoomCommand
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

    public async Task<UpdateRoomResult> UpdateRoom(Guid roomId, UpdateRoomRequest request)
    {
        logger.LogInformation("Updating room by id: {RoomId}", roomId);

        var room = await mediator.Send(new SmartHome.Application.Operations.Rooms.Queries.GetRoomById.GetRoomByIdQuery { Id = roomId });
        if (room is null)
        {
            logger.LogWarning("Room with id {RoomId} not found. Cannot update room.", roomId);
            return UpdateRoomResult.NotFound;
        }

        RoomType? roomType = null;
        if (request.Type.HasValue)
        {
            roomType = (RoomType)request.Type.Value;
        }

        var command = new UpdateRoomCommand
        {
            Id = roomId,
            Name = request.Name,
            Type = roomType
        };

        var result = await mediator.Send(command);

        return result switch
        {
            UpdateResultStatus.Success => UpdateRoomResult.Success,
            UpdateResultStatus.NotFound => UpdateRoomResult.NotFound,
            UpdateResultStatus.ValidationError => UpdateRoomResult.ValidationError,
            _ => UpdateRoomResult.Failure
        };
    }
}