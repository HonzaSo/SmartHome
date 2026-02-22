using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Queries;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class GetDeviceByIdQueryHandler (IDeviceRepository deviceRepository) : IRequestHandler<GetDeviceByIdQuery, Device?>
{
    public async Task<Device?> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        return await deviceRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}