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
        
        return homes.Select(HomeMapper.MapToDomain)
             .Where(h => h != null)
             .Cast<Home>()
             .ToList();
    }

    public async Task<Home?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var homeEntity = await context.Homes.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        return HomeMapper.MapToDomain(homeEntity);
    }
    
    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.Homes
            .Where(h => h.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> HasRoomsAsync(Guid homeId, CancellationToken cancellationToken)
    {
        return context.Rooms.AnyAsync(r => r.HomeId == homeId, cancellationToken);
    }
}