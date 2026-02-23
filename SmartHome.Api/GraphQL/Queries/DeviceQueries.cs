using MediatR;
using SmartHome.Application.Queries;
using SmartHomeApi.GraphQL.Dtos.Devices;
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
            return new GetErrorResult("Room not found.", ErrorCategory.EntityNotFound, "Room");
        }
        
        return new DevicesListResponse()
        {
            Devices = devices.Select(DeviceTypeMapper.MapFromDomain).ToList()
        };
    }

    public async Task<IGetDeviceResult> GetDeviceById(Guid deviceId)
    {
        logger.LogInformation("Getting device for deviceId {DeviceId}", deviceId);

        var device = await mediator.Send(new GetDeviceByIdQuery { Id = deviceId });

        if (device == null)
        {
            return new GetErrorResult("Device not found.", ErrorCategory.EntityNotFound, "Device");
        }

        return DeviceTypeMapper.MapFromDomain(device);
    }
}

