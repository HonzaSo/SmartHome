namespace SmartHome.Infrastructure.Entities;

public class RoomEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Type { get; set; }

    public Guid HomeId { get; set; }
    public HomeEntity HomeEntity { get; set; } = null!;
    public ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
}