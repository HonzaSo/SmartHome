using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Devices;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType("Query")]
public class DeviceQueries (IMediator mediator, ILogger<DeviceQueries> logger)
{
    public async Task<List<DeviceTypeResponse>> GetDevicesByRoomId(Guid roomId)
    {
        logger.LogInformation("Getting devices for roomId {RoomId}", roomId);

        var devices = await mediator.Send(new GetDevicesByRoomIdQuery { Id = roomId });
        return devices.Select(DeviceTypeMapper.MapFromDomain).ToList();
    }
}

