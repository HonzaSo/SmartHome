using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Dtos.Devices;

public class UpdateDeviceRequest
{
    public string? Name { get; set; }
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public DeviceStateEnum? State { get; set; }
}

