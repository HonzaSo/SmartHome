using SmartHome.Domain.Domains;

namespace SmartHome.Application.Interfaces;

public interface IRoomRepository
{
    Task<Guid> AddAsync(Guid homeId, Room room, CancellationToken cancellationToken);
}