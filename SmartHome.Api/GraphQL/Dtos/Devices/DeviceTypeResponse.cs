using SmartHomeApi.GraphQL.Interfaces;
using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Dtos.Devices;

public class DeviceTypeResponse : IGetRoomResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public DeviceStateEnum State { get; set; }
}

