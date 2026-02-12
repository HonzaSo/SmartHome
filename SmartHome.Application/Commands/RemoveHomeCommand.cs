using MediatR;
using SmartHome.Application.Handlers;

namespace SmartHome.Application.Commands;

public record RemoveHomeCommand(Guid homeId) : IRequest<DeleteResultStatus>
{
    public Guid Id { get; init; } = homeId;
}