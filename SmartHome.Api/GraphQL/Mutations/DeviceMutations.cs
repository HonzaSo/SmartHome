using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Dtos.Devices;

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
}
