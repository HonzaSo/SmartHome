using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Types;

public class RoomTypeInput
{
    public string Name { get; set; }
    public RoomTypeEnum Type { get; set; }
    public Guid HomeId { get; set; }
}