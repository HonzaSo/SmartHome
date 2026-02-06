using SmartHome.Domain.Domains;
using SmartHome.Infrastructure.Entities;

namespace SmartHome.Infrastructure.Mappers;

public static class HomeMapper
{
    public static Home? MapToDomain(HomeEntity? homeEntity)
    {
        if (homeEntity == null) return null;
        
        var homeDomain = new Home
        {
            Id = homeEntity.Id,
            Name = homeEntity.Name,
            Address = new Domain.Domains.Address()
            {
                Street = homeEntity.Address.Street,
                City = homeEntity.Address.City,
                ZipCode = homeEntity.Address.ZipCode
            }
        };

        return homeDomain;
    }
}