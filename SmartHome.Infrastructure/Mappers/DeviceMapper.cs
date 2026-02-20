using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;
using SmartHome.Domain.Helpers;
using SmartHome.Infrastructure.Entities;

namespace SmartHome.Infrastructure.Mappers;

public static class DeviceMapper
{
    public static Device? MapToDomain(DeviceEntity? deviceEntity)
    {
        if (deviceEntity == null)
        {
            return null;
        }

        return new Device
        {
            Id = deviceEntity.Id,
            Name = deviceEntity.Name,
            Model = deviceEntity.Model,
            Manufacturer = deviceEntity.Manufacturer,
            State = EnumHelper.ToEnum<DeviceState>(deviceEntity.State) ?? DeviceState.Offline
        };
    }

    public static DeviceEntity MapToEntity(Device device, Guid roomId)
    {
        return new DeviceEntity
        {
            Id = device.Id == Guid.Empty ? Guid.NewGuid() : device.Id,
            Name = device.Name,
            Model = device.Model,
            Manufacturer = device.Manufacturer,
            State = device.State.ToString(),
            RoomId = roomId
        };
    }
}

