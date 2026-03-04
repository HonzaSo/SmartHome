using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Devices.Queries.GetDeviceByRoomId;

public class GetDevicesByRoomIdQuery : IRequest<List<Device>?>
{
    public Guid Id { get; set; }
}

