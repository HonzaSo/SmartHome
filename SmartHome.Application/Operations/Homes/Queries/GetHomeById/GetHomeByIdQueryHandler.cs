using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Homes.Queries.GetHomeById;

public class GetHomeByIdQueryHandler(IHomeRepository homeRepository) : IRequestHandler<GetHomeByIdQuery, Home?>
{
    public async Task<Home?> Handle(GetHomeByIdQuery request, CancellationToken cancellationToken)
    {
        return await homeRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}