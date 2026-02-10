namespace SmartHome.Domain.Helpers;

public class EnumHelper
{
    public static T? ToEnum<T>(string value) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, ignoreCase: true, out T result))
        {
            return result;
        }
        
        return null;
    }
}