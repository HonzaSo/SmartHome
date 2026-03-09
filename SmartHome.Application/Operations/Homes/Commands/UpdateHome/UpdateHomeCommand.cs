using MediatR;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Operations.Homes.Commands.UpdateHome;

public record UpdateHomeCommand : IRequest<UpdateResultStatus>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? ZipCode { get; init; }
}

