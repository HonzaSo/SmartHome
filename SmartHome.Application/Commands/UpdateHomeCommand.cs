using MediatR;
using SmartHome.Application.Enums;

namespace SmartHome.Application.Commands;

public record UpdateHomeCommand : IRequest<UpdateResult>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? ZipCode { get; init; }
}

