using SmartHomeApi.GraphQL.Dtos.Rooms;

namespace SmartHomeApi.GraphQL.Interfaces;

public interface IGetRoomsResult
{
    
}

public class RoomsListResponse : IGetRoomsResult
{
    public List<RoomTypeResponse> Rooms { get; set; } = new();
}