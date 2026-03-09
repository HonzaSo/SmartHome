using SmartHomeApi.GraphQL.Dtos.Rooms;

namespace SmartHomeApi.GraphQL.Interfaces;

public interface IGetRoomsResult
{
    
}

public class RoomsListResponse : IGetRoomsResult
{
    public List<GetRoomResponse> Rooms { get; set; } = new();
}