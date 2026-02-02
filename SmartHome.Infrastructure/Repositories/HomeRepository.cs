using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;
using SmartHome.Infrastructure.Entities;

namespace SmartHome.Infrastructure.Repositories;

public class HomeRepository(ApplicationDbContext context) : IHomeRepository
{
    public async Task<Guid> AddAsync(Home homeDomain, CancellationToken ct)
    {
        var dbEntity = new HomeEntity
        {
            Id = homeDomain.Id,
            Name = homeDomain.Name,
            Address = new Entities.Address() 
            {
                Street = homeDomain.Address.Street,
                City = homeDomain.Address.City,
                ZipCode = homeDomain.Address.ZipCode
            }
        };
        
        context.Homes.Add(dbEntity);
        await context.SaveChangesAsync(ct);
        return dbEntity.Id;
    }
}