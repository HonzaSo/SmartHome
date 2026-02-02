using MediatR;

namespace SmartHome.Application.Commands;

public record CreateHomeCommand(
    string Name, 
    string Street, 
    string City, 
    string ZipCode
) : IRequest<Guid>;