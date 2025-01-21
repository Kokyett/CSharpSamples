using System.Reflection;

namespace CSharpSamples.Services;

public static class Application
{
    public static DirectoryInfo LogDirectory { get; }
    public static DirectoryInfo TempDirectory { get; }
    
    static Application()
    {
        string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!;
        
        LogDirectory = new DirectoryInfo(Path.Combine(Path.Combine(basePath, "Logs")));
        if (!LogDirectory.Exists)
            LogDirectory.Create();
        
        TempDirectory = new DirectoryInfo(Path.Combine(Path.Combine(basePath, "Temp")));
        if (!TempDirectory.Exists)
            TempDirectory.Create();
    }    
}