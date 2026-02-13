using SmartHome.Domain.Domains;

namespace SmartHome.Infrastructure.Entities;

public sealed class HomeEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Address Address { get; set; }
    
    public ICollection<RoomEntity> Rooms { get; set; } = new List<RoomEntity>();
}