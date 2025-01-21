namespace CSharpSamples.Services.Logging;

public class ConsoleLogger : ILogger
{
    private static readonly ConsoleColor DefaultColor = Console.ForegroundColor;
    private readonly Lock _locker = new();
    
    public LogLevel LogLevel { get; }

    public ConsoleLogger(LogLevel logLevel)
    {
        LogLevel = logLevel;
    }
    
    private ConsoleColor MapColor(LogType logType)
    {
        return logType switch
        {
            LogType.Important => ConsoleColor.DarkCyan,
            LogType.Success => ConsoleColor.Green,
            LogType.Warning => ConsoleColor.DarkYellow,
            LogType.Error => ConsoleColor.DarkRed,
            _ => DefaultColor,
        };
    }
    
    public void Write(string message, LogType logType, LogLevel logLevel)
    {
        if (logLevel > LogLevel)
            return;
        
        lock (_locker)
        {
            Console.ForegroundColor = MapColor(logType);
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public void Write(LogLines lines)
    {
        lock (_locker)
        {
            foreach (var line in lines)
            {
                if (line.LogLevel > LogLevel)
                    continue;
                Console.ForegroundColor = MapColor(line.LogType);
                Console.WriteLine(line.Message);
                Console.ResetColor();
            }
        }
    }

    public void Dispose()
    {
        // Nothing
    }
}