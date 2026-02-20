namespace SmartHome.Domain.Domains;

using SmartHome.Domain.Enums;

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public DeviceState State { get; set; }
}

