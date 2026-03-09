using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Dtos.Rooms;

public class UpdateRoomRequest
{
    public string? Name { get; set; }
    public RoomTypeEnum? Type { get; set; }
}

