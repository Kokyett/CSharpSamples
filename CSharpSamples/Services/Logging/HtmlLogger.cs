using System.Net.Mime;
using System.Reflection;

namespace CSharpSamples.Services.Logging;

public class HtmlLogger : ILogger
{
    private readonly Lock _locker = new();
    private readonly StreamWriter _streamWriter;
    public LogLevel LogLevel { get; }

    public HtmlLogger(LogLevel logLevel)
    {
        LogLevel = logLevel;
        _streamWriter = OpenStream();
    }

    private static StreamWriter OpenStream()
    {
        var fi = new FileInfo(Path.Combine(
            Application.LogDirectory.FullName,
            $"Log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.html"
        ));
        if (!fi.Directory!.Exists)
            fi.Directory.Create();

        var sw = new StreamWriter(fi.FullName) { AutoFlush = true }; 
        sw.WriteLine("<html>");
        sw.WriteLine("<head>");
        sw.WriteLine($"<title>{Assembly.GetEntryAssembly()?.GetName().Name}</title>");
        sw.WriteLine("<style>");
        sw.WriteLine("body { background: black; font-family: consolas; color: white; }");
        sw.WriteLine(".Important { color: cyan; }");
        sw.WriteLine(".Success { color: green; }");
        sw.WriteLine(".Warning { color: orange; }");
        sw.WriteLine(".Error { color: red; }");
        sw.WriteLine("</style>");
        sw.WriteLine("</head>");
        sw.WriteLine("<body>");
        sw.WriteLine("<pre>");
        return sw;
    }
    
    public void Write(string message, LogType logType, LogLevel logLevel)
    {
        if (logLevel > LogLevel)
            return;
        
        lock (_locker)
            _streamWriter.WriteLine($"<span class=\"{logType}\">{message}</span>");
    }

    public void Write(LogLines lines)
    {
        lock (_locker)
        {
            foreach (var line in lines)
            {
                if (line.LogLevel > LogLevel)
                    continue;
                _streamWriter.WriteLine($"<span class=\"{line.LogType}\">{line.Message}</span>");
            }
        }
    }
   
    public void Dispose()
    {
        _streamWriter.WriteLine("</pre>");
        _streamWriter.WriteLine("</body>");
        _streamWriter.WriteLine("</html>");
        _streamWriter.Dispose();
    }
}