namespace SmartHome.Infrastructure.Entities;

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public string State { get; set; }

    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;
    
}