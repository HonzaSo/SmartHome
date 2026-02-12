using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Dtos.Rooms;

public class RoomTypeRequest
{
    public required string Name { get; set; }
    public required RoomTypeEnum Type { get; set; }
    public required Guid HomeId { get; set; }
}