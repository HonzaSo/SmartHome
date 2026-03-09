using SmartHome.Domain.Domains;
using SmartHome.Domain.Helpers;
using SmartHomeApi.GraphQL.Dtos.Rooms;
using SmartHomeApi.GraphQL.Enums;

namespace SmartHomeApi.Mappers;

public static class RoomTypeMapper
{
    public static GetRoomResponse MapFromDomain(Room room)
    {
        return new GetRoomResponse
        {
            Id = room.Id,
            Name = room.Name,
            Type = EnumHelper.ToEnum<RoomTypeEnum>(room.Type.ToString()) ?? RoomTypeEnum.Unknown
        };
    }

}