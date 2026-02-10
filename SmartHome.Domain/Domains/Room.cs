using SmartHome.Domain.Enums;

namespace SmartHome.Domain.Domains;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RoomType Type { get; set; }
}