using MediatR;
using SmartHome.Application.Interfaces;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Homes.Queries.GetHomes;

public class GetAllHomesQueryHandler(IHomeRepository homeRepository) : IRequestHandler<GetAllHomesQuery, List<Home>>
{
    public async Task<List<Home>> Handle(GetAllHomesQuery request, CancellationToken cancellationToken)
    {
        return await homeRepository.GetAllHomesAsync(cancellationToken);
    }
}