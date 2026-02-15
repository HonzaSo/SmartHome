using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Queries;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class GetHomeByIdQueryHandler(IHomeRepository homeRepository) : IRequestHandler<GetHomeByIdQuery, Home?>
{
    public async Task<Home?> Handle(GetHomeByIdQuery request, CancellationToken cancellationToken)
    {
        return await homeRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}