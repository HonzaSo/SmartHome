using SmartHomeApi.GraphQL.Dtos.Devices;

namespace SmartHomeApi.GraphQL.Interfaces;

public interface IGetDevicesResult
{
}

public class DevicesListResponse : IGetDevicesResult
{
    public List<DeviceTypeResponse> Devices { get; set; } = new();
}