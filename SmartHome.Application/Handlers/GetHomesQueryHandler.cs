using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Queries;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Handlers;

public class GetHomesQueryHandler(IHomeRepository homeRepository) : IRequestHandler<GetAllHomesQuery, List<Home>>
{
    public async Task<List<Home>> Handle(GetAllHomesQuery request, CancellationToken cancellationToken)
    {
        return await homeRepository.GetAllHomesAsync(cancellationToken);
    }
}