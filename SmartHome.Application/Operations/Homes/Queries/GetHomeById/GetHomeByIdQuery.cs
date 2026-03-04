using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Homes.Queries.GetHomeById;

public class GetHomeByIdQuery : IRequest<Home?>
{
    public Guid Id { get; set; }
}