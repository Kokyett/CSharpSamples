namespace CSharpSamples.Services;

public static class Application
{
    public static DirectoryInfo LogDirectory { get; }
    public static DirectoryInfo OutDirectory { get; }
    public static DirectoryInfo TempDirectory { get; }
    
    static Application()
    {
        string basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!;
        
        LogDirectory = new DirectoryInfo(Path.Combine(basePath, "Logs"));
        if (!LogDirectory.Exists)
            LogDirectory.Create();
        
        OutDirectory = new DirectoryInfo(Path.Combine(basePath, "Out"));
        if (!OutDirectory.Exists)
            OutDirectory.Create();
        
        TempDirectory = new DirectoryInfo(Path.Combine(basePath, "Temp"));
        if (!TempDirectory.Exists)
            TempDirectory.Create();
    }    
}