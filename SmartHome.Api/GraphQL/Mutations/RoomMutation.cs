using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Dto;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class RoomMutations (IMediator mediator, ILogger<RoomMutations> logger)
{
    public async Task<Guid> CreateRoom(RoomTypeRequest request)
    {
        logger.LogInformation("Creating room {RoomName}", request.Name);
        var domainRoomType = (RoomType)request.Type;
        
        var command = new CreateRoomCommand()
        {
            Name = request.Name,
            Type =  domainRoomType,
            HomeId = request.HomeId
        };
        
        return await mediator.Send(command);
    }
}