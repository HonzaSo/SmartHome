using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class CreateDeviceCommandHandler(IDeviceRepository deviceRepository) : IRequestHandler<CreateDeviceCommand, Guid>
{
    public async Task<Guid> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = new Device()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Model = request.Model,
            Manufacturer = request.Manufacturer,
            State = request.State
        };

        return await deviceRepository.AddAsync(request.RoomId, device, cancellationToken);
    }
}
