using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Rooms.Queries.GetRoomById;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Devices.Queries.GetDeviceByRoomId;

public class GetDevicesByRoomIdQueryHandler(IDeviceRepository deviceRepository, IMediator mediator) : IRequestHandler<GetDevicesByRoomIdQuery, List<Device>?>
{
    public async Task<List<Device>?> Handle(GetDevicesByRoomIdQuery request, CancellationToken cancellationToken)
    {
        var room = await mediator.Send(new GetRoomByIdQuery { Id = request.Id }, cancellationToken);
        if (room == null)
        {
            return null;
        }

        return await deviceRepository.GetAllByRoomIdAsync(request.Id, cancellationToken);
    }
}
