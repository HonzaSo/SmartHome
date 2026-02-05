using SmartHome.Domain.Domains;
using SmartHomeApi.GraphQL.Types;

namespace SmartHomeApi.Mappers;

public static class HomeTypeMapper
{
    public static GetHomeType MapFromDomain(Home home)
    {
        return new GetHomeType()
        {
            Id = home.Id,
            Name =  home.Name,
            Street = home.Address.Street,
            City = home.Address.City,
            ZipCode = home.Address.ZipCode
            
        };
    }
}