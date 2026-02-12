using MediatR;
using SmartHome.Application.Handlers;

namespace SmartHome.Application.Commands;

public record RemoveHomeCommand(Guid Id) : IRequest<DeleteResultStatus>
{
    public Guid Id { get; init; } = Id;
}