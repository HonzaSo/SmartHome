using SmartHome.Domain.Domains;

namespace SmartHome.Application.Interfaces;

public interface IHomeRepository
{
    Task<Guid> AddAsync(Home home, CancellationToken cancellationToken);
    Task<List<Home>> GetAllHomesAsync(CancellationToken cancellationToken);
    Task<Home?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> HasRoomsAsync(Guid homeId, CancellationToken cancellationToken);
}