using MediatR;
using SmartHome.Domain.Domains;

namespace SmartHome.Application.Queries;

public record GetAllHomesQuery : IRequest<List<Home>>;