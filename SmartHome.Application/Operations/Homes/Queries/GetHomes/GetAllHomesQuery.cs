using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Operations.Homes.Queries.GetHomes;

public record GetAllHomesQuery : IRequest<List<Home>>;