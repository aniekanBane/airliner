namespace Domain.Infrastructure.Storage;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public sealed class FilePathAttribute(string path) : Attribute
{
    public string Path { get; } = path;
}
