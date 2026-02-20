using Microsoft.EntityFrameworkCore;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;
using SmartHome.Infrastructure.Mappers;

namespace SmartHome.Infrastructure.Repositories;

public class DeviceRepository(ApplicationDbContext context) : IDeviceRepository
{
    public async Task<Guid> AddAsync(Guid roomId, Device device, CancellationToken cancellationToken)
    {
        var deviceEntity = DeviceMapper.MapToEntity(device, roomId);
        context.Devices.Add(deviceEntity);
        await context.SaveChangesAsync(cancellationToken);
        return deviceEntity.Id;
    }

    public async Task<List<Device>> GetAllByRoomIdAsync(Guid roomId, CancellationToken cancellationToken)
    {
        var deviceEntities = await context.Devices
            .Where(d => d.RoomId == roomId)
            .ToListAsync(cancellationToken);

        return deviceEntities.Select(DeviceMapper.MapToDomain)
            .Where(x => x != null)
            .Cast<Device>()
            .ToList();
    }

    public async Task<Device?> GetByIdAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        var entity = await context.Devices.FirstOrDefaultAsync(d => d.Id == deviceId, cancellationToken);
        return DeviceMapper.MapToDomain(entity);
    }
}
