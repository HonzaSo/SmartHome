using SmartHome.Domain.Domains;

namespace SmartHome.Application.Interfaces;

public interface IDeviceRepository
{
    Task<Guid> AddAsync(Guid roomId, Device device, CancellationToken cancellationToken);
    Task<List<Device>?> GetAllByRoomIdAsync(Guid roomId, CancellationToken cancellationToken);
    Task<Device?> GetByIdAsync(Guid deviceId, CancellationToken cancellationToken);
}
