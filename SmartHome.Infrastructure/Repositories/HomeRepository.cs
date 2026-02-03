using Microsoft.EntityFrameworkCore;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;
using SmartHome.Infrastructure.Entities;
using SmartHome.Infrastructure.Mappers;

namespace SmartHome.Infrastructure.Repositories;

public class HomeRepository(ApplicationDbContext context) : IHomeRepository
{
    public async Task<Guid> AddAsync(Home homeDomain, CancellationToken cancellationToken)
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
        await context.SaveChangesAsync(cancellationToken);
        return dbEntity.Id;
    }

    public async Task<List<Home>> GetAllHomesAsync(CancellationToken cancellationToken)
    {
        var homes = await context.Homes.AsNoTracking().ToListAsync(cancellationToken);
        
        return homes.Select(HomeMapper.MapToDomain).ToList();
    }
}