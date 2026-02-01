namespace SmartHome.Infrastructure.Entities;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public Guid HomeId { get; set; }
    public virtual Home Home { get; set; } = null!;
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}