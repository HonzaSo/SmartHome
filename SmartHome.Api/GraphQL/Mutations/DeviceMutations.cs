using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Application.Queries;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Dtos.Devices;
using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Errors;
using SmartHomeApi.GraphQL.Interfaces;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class DeviceMutations (IMediator mediator, ILogger<DeviceMutations> logger)
{
    public async Task<ICreateDeviceResult> CreateDevice(CreateDeviceRequest request)
    {
        logger.LogInformation("Creating device {DeviceName} in room {RoomId}", request.Name, request.RoomId);
        var room = await mediator.Send(new GetRoomByIdQuery { Id = request.RoomId });
        
        if (room is null)
        {
            logger.LogWarning("Room with id {RoomId} not found. Cannot create device.", request.RoomId);
            return new GetErrorResult("Room not found.", ErrorCategory.EntityNotFound, "Room");
        }
        
        var domainState = (DeviceState)request.State;

        var command = new CreateDeviceCommand
        {
            RoomId = request.RoomId,
            Name = request.Name,
            Model = request.Model,
            Manufacturer = request.Manufacturer,
            State = domainState
        };

        var result = await mediator.Send(command);
        return new CreateDeviceSuccess(result);
    }

    public async Task<DeviceRemovalResult> RemoveDeviceById(Guid deviceId)
    {
        logger.LogInformation("Removing device by id: {DeviceId}", deviceId);

        var request = new RemoveDeviceCommand
        {
            Id = deviceId
        };

        var result = await mediator.Send(request);

        return result switch
        {
            DeleteResultStatus.Deleted => DeviceRemovalResult.Success,
            DeleteResultStatus.NotFound => DeviceRemovalResult.NotFound,
            _ => DeviceRemovalResult.Failure
        };
    }
}
