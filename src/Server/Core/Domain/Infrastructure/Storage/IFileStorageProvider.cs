namespace Domain.Infrastructure.Storage;

public interface IFileStorageProvider
{
    Task ClearAsync(CancellationToken cancellationToken = default);

    Task DeleteAsync(
        IFileEntry fileEntry, 
        CancellationToken cancellationToken = default
    );

    Task<byte[]> ReadByteArrayAsync(
        IFileEntry fileEntry,
        CancellationToken cancellationToken = default
    );

    Task<string> ReadStringAsync(
        IFileEntry fileEntry,
        CancellationToken cancellationToken = default
    );

    Task UploadAsync(
        IFileEntry fileEntry,
        Stream stream, 
        CancellationToken cancellationToken = default
    );
}
