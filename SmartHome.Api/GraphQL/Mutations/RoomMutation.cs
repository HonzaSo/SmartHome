using MediatR;
using SmartHome.Application.Commands;
using SmartHome.Domain.Enums;
using SmartHomeApi.GraphQL.Types;

namespace SmartHomeApi.GraphQL.Mutations;

[ExtendObjectType(typeof(Mutation))]
public class RoomMutations (IMediator mediator, ILogger<RoomMutations> logger)
{
    public async Task<Guid> CreateRoom(RoomTypeInput input)
    {
        logger.LogInformation("Creating room {RoomName}", input.Name);
        var domainRoomType = (RoomType)input.Type;
        
        var request = new CreateRoomCommand()
        {
            Name = input.Name,
            Type =  domainRoomType,
            HomeId = input.HomeId
        };
        
        return await mediator.Send(request);
    }
}