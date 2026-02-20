using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Queries;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class GetDevicesByRoomIdQueryHandler(IDeviceRepository deviceRepository) : IRequestHandler<GetDevicesByRoomIdQuery, List<Device>?>
{
    public async Task<List<Device>?> Handle(GetDevicesByRoomIdQuery request, CancellationToken cancellationToken)
    {
        return await deviceRepository.GetAllByRoomIdAsync(request.Id, cancellationToken);
    }
}
