using MediatR;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Operations.Homes.Commands.RemoveHome;

public record RemoveHomeCommand : IRequest<DeleteResultStatus>
{
    public Guid Id { get; init; }
}