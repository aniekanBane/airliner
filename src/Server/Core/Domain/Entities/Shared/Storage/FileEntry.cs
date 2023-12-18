using Domain.Infrastructure.Storage;
using Domain.Primitives.Common;

namespace Domain.Entities.Shared.Storage;

public sealed class FileEntry : Entity<Guid>, IAggregateRoot, IFileEntry
{
    private FileEntry(
        long fileSize, 
        string altName, 
        string fileName, 
        string description,
        FileType fileType)
    {
        FileSize = fileSize;
        AltName = altName;
        FileName = fileName;
        FileType = fileType;
        Description = description;
        FileLocation = GetLocation(fileName);
    }

    public long FileSize { get; init; }

    public string? AltName { get; init; }

    public string FileName { get; init; }

    public string FileLocation { get; init; }

    public string? Description { get; init; }

    public FileType FileType { get; init; }

    public DateTime Uploaded { get; init; }

    private static string GetLocation(string fileName) => 
        Path.Combine(
            $"{DateTime.Now:yyyy/MM}", 
            $"{Guid.NewGuid()}{Path.GetExtension(fileName)}");

    public static FileEntry Create(
        long fileSize, 
        string fileName,
        FileType fileType, 
        string? altName = default,
        string? description = default
    )
    {
        altName ??= string.Empty;
        description ??= string.Empty;
        fileName = Path.GetFileName(fileName);

        return new FileEntry(fileSize, altName, fileName, description, fileType);
    }
}
