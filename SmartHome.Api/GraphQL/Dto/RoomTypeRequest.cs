using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.GraphQL.Dto;

public class RoomTypeRequest
{
    public string Name { get; set; }
    public RoomTypeEnum Type { get; set; }
    public Guid HomeId { get; set; }
}