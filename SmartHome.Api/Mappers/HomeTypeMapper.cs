using SmartHome.Domain.Domains;
using SmartHomeApi.GraphQL.Dtos.Homes;

namespace SmartHomeApi.Mappers;

public static class HomeTypeMapper
{
    public static HomeTypeResponse MapFromDomain(Home home)
    {
        return new HomeTypeResponse()
        {
            Id = home.Id,
            Name =  home.Name,
            Street = home.Address.Street,
            City = home.Address.City,
            ZipCode = home.Address.ZipCode
            
        };
    }
}