using MediatR;

namespace SmartHome.Application.Commands;

public record CreateHomeCommand : IRequest<Guid>
{
    public string Name { get; init; }
    public string Street { get; init; }
    public string City { get; init; }
    public string ZipCode { get; init; }
};