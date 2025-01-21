using CSharpSamples.Services.Logging;

namespace CSharpSamples.Services;

public static class Downloader
{
    public static byte[] DownloadCache(string url, string filename, TimeSpan timeSpan)
    {
        FileInfo file = new(Path.Combine(Application.TempDirectory.FullName, filename));
        if (file.Exists && file.LastWriteTime > DateTime.Now.Add(-timeSpan))
        {
            Log.Write($"Récupération du fichier {file.FullName}", LogType.Important, LogLevel.Three);
            return File.ReadAllBytes(file.FullName);
        }
        
        Log.Write($"Téléchargement de {url}", LogType.Important, LogLevel.Three);
        using HttpClient client = new();
        var bytes = client.GetByteArrayAsync(url).Result;
        File.WriteAllBytes(file.FullName, bytes);
        return bytes;
    }
}