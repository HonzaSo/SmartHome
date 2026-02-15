namespace SmartHomeApi.GraphQL.Enums;

public enum RoomTypeEnum
{
    [GraphQLName("Unknown")]
    Unknown,
    [GraphQLName("LivingRoom")]
    LivingRoom,
    [GraphQLName("Kitchen")]
    Kitchen,
    [GraphQLName("Bedroom")]
    Bedroom,
    [GraphQLName("Bathroom")]
    Bathroom,
    [GraphQLName("Garage")]
    Garage
}