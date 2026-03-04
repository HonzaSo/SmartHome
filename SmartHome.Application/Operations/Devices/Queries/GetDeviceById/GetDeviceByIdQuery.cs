using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Devices.Queries.GetDeviceById;

public class GetDeviceByIdQuery : IRequest<Device?>
{
    public Guid Id { get; set; }
    
}