using SmartHome.Domain.Domains;
using SmartHome.Domain.Helpers;
using SmartHomeApi.GraphQL.Dtos.Devices;
using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.Mappers;

public static class DeviceTypeMapper
{
    public static GetDeviceResponse MapFromDomain(Device device)
    {
        return new GetDeviceResponse
        {
            Id = device.Id,
            Name = device.Name,
            Model = device.Model,
            Manufacturer = device.Manufacturer,
            State = EnumHelper.ToEnum<DeviceStateEnum>(device.State.ToString()) ?? DeviceStateEnum.Offline
        };
    }
}

