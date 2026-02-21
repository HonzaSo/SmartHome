using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Errors;
using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.Mappers;

namespace SmartHomeApi.GraphQL.Queries;

[ExtendObjectType("Query")]
public class DeviceQueries (IMediator mediator, ILogger<DeviceQueries> logger)
{
    public async Task<IGetDevicesResult> GetDevicesByRoomId(Guid roomId)
    {
        logger.LogInformation("Getting devices for roomId {RoomId}", roomId);

        var devices = await mediator.Send(new GetDevicesByRoomIdQuery { Id = roomId });
        
        if (devices == null)
        {
            return new GetErrorResult("Room not found.", ErrorCategory.InputNotFound, "Room");
        }
        
        return new DevicesListResponse()
        {
            Devices = devices.Select(DeviceTypeMapper.MapFromDomain).ToList()
        };
    }
}

