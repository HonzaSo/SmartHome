using Microsoft.EntityFrameworkCore;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;
using SmartHome.Infrastructure.Entities;
using SmartHome.Infrastructure.Mappers;

namespace SmartHome.Infrastructure.Repositories;

public class RoomRepository (ApplicationDbContext context) : IRoomRepository
{
    public async Task<Guid> AddAsync(Guid homeId, Room room, CancellationToken cancellationToken)
    {
        var roomEntity = new RoomEntity()
        {
            Id =  Guid.NewGuid(),
            Name = room.Name,
            Type = room.Type.ToString(),
            HomeId =  homeId
        };

        context.Rooms.Add(roomEntity);
        await context.SaveChangesAsync(cancellationToken);
        return roomEntity.Id;
    }

    public async Task<List<Room>> GetAllRoomsByHomeIdAsync(Guid homeId, CancellationToken cancellationToken)
    {
        var roomEntities = await context.Rooms
            .Where(r => r.HomeId == homeId)
            .ToListAsync(cancellationToken);
            
        return roomEntities.Select(RoomMapper.MapToDomain)
            .Where(x => x != null)
            .Cast<Room>()
            .ToList();
    }

    public async Task<Room?> GetRoomByIdAsync(Guid roomId, CancellationToken cancellationToken)
    {
        var room = await context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId, cancellationToken);
        return RoomMapper.MapToDomain(room);
    }
}