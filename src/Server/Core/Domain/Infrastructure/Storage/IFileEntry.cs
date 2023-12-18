namespace Domain.Infrastructure.Storage;

public interface IFileEntry
{
    string FileName { get; }
    string FileLocation { get; }
    FileType FileType { get; }
}

public enum FileType
{
    [FilePath(@"images")]
    Image = 61,

    [FilePath(@"profile-images")]
    ProfileImage,

    [FilePath(@"videos")]
    Video,

    [FilePath(@"documents")]
    Document
}