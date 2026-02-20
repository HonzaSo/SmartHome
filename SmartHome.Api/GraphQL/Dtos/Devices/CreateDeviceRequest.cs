namespace SmartHomeApi.GraphQL.Dtos.Devices;

using SmartHomeApi.GraphQL.Enums;

public class CreateDeviceRequest
{
    public Guid RoomId { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public DeviceStateEnum State { get; set; }
}

