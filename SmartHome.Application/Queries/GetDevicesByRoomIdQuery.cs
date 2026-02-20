using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Queries;

public class GetDevicesByRoomIdQuery : IRequest<List<Device>?>
{
    public Guid Id { get; set; }
}

