using SmartHomeApi.GraphQL.Enums;
using SmartHomeApi.GraphQL.Interfaces;

namespace SmartHomeApi.GraphQL.Dtos.Rooms;

public class RoomTypeResponse : IGetRoomResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RoomTypeEnum Type { get; set; }
}