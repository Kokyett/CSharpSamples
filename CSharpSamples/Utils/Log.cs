namespace CSharpSamples.Utils;

public enum LogType
{
    Highlight,
    Information,
    Warning,
    Error
}

public class Log
{
    public static void Write(string message, LogType type = LogType.Information)
    {
        Console.ForegroundColor = Map(type);
        Console.Write(message);
        Console.ResetColor();
    }
    
    public static void WriteLine(string message, LogType type = LogType.Information)
    {
        Console.ForegroundColor = Map(type);
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void WriteLine(Exception e, LogType type = LogType.Error)
    {
        Console.ForegroundColor = Map(type);
        Console.WriteLine($"Exception: {e.GetType().FullName}");

        if (!string.IsNullOrWhiteSpace(e.Message))
            Console.WriteLine($"  - {e.Message}");

        if (!string.IsNullOrWhiteSpace(e.StackTrace))
        {
            var lines = e.StackTrace.Split(Environment.NewLine).Select(x => x.Trim());
            var stackTrace = string.Join($"{Environment.NewLine}  - ", lines);
            Console.WriteLine($"  - {stackTrace}");
        }
        
        Console.ResetColor();
    }

    private static ConsoleColor Map(LogType type)
    {
        return type switch
        {
            LogType.Highlight => ConsoleColor.Cyan,
            LogType.Warning => ConsoleColor.Yellow,
            LogType.Error => ConsoleColor.Red,
            _ => Console.ForegroundColor
        };
    }
}