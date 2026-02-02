namespace SmartHome.Infrastructure.Entities;

public class RoomEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public Guid HomeId { get; set; }
    public virtual HomeEntity HomeEntity { get; set; } = null!;
    public virtual ICollection<DeviceEntity> Devices { get; set; } = new List<DeviceEntity>();
}