using SmartHome.Domain.Domains;

namespace SmartHome.Application.Interfaces;

public interface IRoomRepository
{
    Task<Guid> AddAsync(Guid homeId, Room room, CancellationToken cancellationToken);
    Task<List<Room>?> GetAllRoomsByHomeIdAsync(Guid homeId, CancellationToken cancellationToken);
    Task<Room?> GetRoomByIdAsync(Guid roomId, CancellationToken cancellationToken);
}