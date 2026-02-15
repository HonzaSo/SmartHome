using SmartHome.Domain.Domains;
using SmartHome.Domain.Enums;
using SmartHome.Domain.Helpers;
using SmartHome.Infrastructure.Entities;

namespace SmartHome.Infrastructure.Mappers;

public static class RoomMapper
{
    public static Room? MapToDomain(RoomEntity? roomEntity)
    {

        if (roomEntity == null)
        {
            return null;
        }
        
        return new Room
        {
            Id = roomEntity.Id,
            Name = roomEntity.Name,
            Type = EnumHelper.ToEnum<RoomType>(roomEntity.Type) ?? RoomType.Unknown
        };
    }
}