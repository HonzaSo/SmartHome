using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class CreateHomeCommandHandler(IHomeRepository homeRepository) : IRequestHandler<CreateHomeCommand, Guid>
{
    public async Task<Guid> Handle(CreateHomeCommand request, CancellationToken cancellationToken)
    {
        var address = new Address()
        {
            Street = request.Street, 
            City = request.City, 
            ZipCode = request.ZipCode
        };
        
        var home = new Home
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Address = address
        };
        
        return await homeRepository.AddAsync(home, cancellationToken);
    }
}