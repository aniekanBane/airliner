using Domain.Infrastructure.Storage;

namespace Domain.Utilities;

public static class EnumExtensions
{
    public static string ToPath(this Enum val)
    {
        var str = val.ToString();
        var fieldInfo = val.GetType().GetField(str);
        var attribute = Attribute.GetCustomAttribute(
            fieldInfo!, typeof(FilePathAttribute)) as FilePathAttribute;

        return attribute switch
        {
            not null => attribute.Path,
            _ => str
        };
    }
}
