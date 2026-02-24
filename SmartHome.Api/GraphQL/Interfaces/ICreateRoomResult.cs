namespace SmartHomeApi.GraphQL.Interfaces;

public interface ICreateRoomResult
{
}

public record CreateRoomSuccess(Guid Id) : ICreateRoomResult;