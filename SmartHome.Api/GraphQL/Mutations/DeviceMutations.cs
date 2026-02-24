using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Application.Enums;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Dtos.Devices;
using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class DeviceMutations (IMediator mediator, ILogger<DeviceMutations> logger)
{
    public async Task<Guid> CreateDevice(CreateDeviceRequest request)
    {
        logger.LogInformation("Creating device {DeviceName} in room {RoomId}", request.Name, request.RoomId);

        var domainState = (DeviceState)request.State;

        var command = new CreateDeviceCommand()
        {
            RoomId = request.RoomId,
            Name = request.Name,
            Model = request.Model,
            Manufacturer = request.Manufacturer,
            State = domainState
        };

        return await mediator.Send(command);
    }

    public async Task<DeviceRemovalResult> RemoveDeviceById(Guid deviceId)
    {
        logger.LogInformation("Removing device by id: {DeviceId}", deviceId);

        var request = new RemoveDeviceCommand()
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
