using SmartHome.Domain.Domains;

namespace SmartHome.Infrastructure.Entities;

public sealed class HomeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    
    public ICollection<RoomEntity> Rooms { get; set; } = new List<RoomEntity>();

    public static Home MapToDomain(HomeEntity homeEntity)
    {
        var homeDomain = new Home();
        homeDomain.Id = homeEntity.Id;
        homeDomain.Name = homeEntity.Name;
        homeDomain.Address = new Domain.Domains.Address()
        {
            Street = homeEntity.Address.Street,
            City = homeEntity.Address.City,
            ZipCode = homeEntity.Address.ZipCode
        };
        
        return homeDomain;
    }
}