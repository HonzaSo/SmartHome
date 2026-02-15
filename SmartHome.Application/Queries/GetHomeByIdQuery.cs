using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Queries;

public class GetHomeByIdQuery : IRequest<Home?>
{
    public Guid Id { get; set; }
}