using SmartHome.Domain.Domains;

namespace SmartHome.Application.Interfaces;

public interface IHomeRepository
{
    Task<Guid> AddAsync(Home home, CancellationToken cancellationToken);
    Task<List<Home>> GetAllHomesAsync(CancellationToken cancellationToken);
}