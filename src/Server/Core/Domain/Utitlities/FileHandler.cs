namespace Domain.Utilities;

public static class FileHandler
{
    public static Stream ByteArrayToStream(byte[] array)
    {
        using var stream = new MemoryStream(array);
        return stream;
    }

    public static Stream DownloadFromLocal(string path)
    {
        using var stream = File.OpenRead(path);
        return stream;
    }

    public static async Task<byte[]> DownloadFromUrlAsync(string url)
    {
        using var httpClient = new HttpClient();
        var uri = new Uri(url).GetLeftPart(UriPartial.Path);
        return await httpClient.GetByteArrayAsync(uri);
    }
}
