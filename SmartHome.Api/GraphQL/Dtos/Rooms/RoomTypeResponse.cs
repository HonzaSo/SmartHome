using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Dtos.Rooms;

public class RoomTypeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public RoomTypeEnum Type { get; set; }
}