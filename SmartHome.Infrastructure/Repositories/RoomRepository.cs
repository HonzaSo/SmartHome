using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;
using SmartHome.Infrastructure.Entities;

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
}