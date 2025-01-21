namespace CSharpSamples.Services.Logging;

public static class Log
{
    private static readonly List<ILogger> Loggers = [];

    public static void RegisterLogger(ILogger logger)
    {
        Loggers.Add(logger);
    }

    public static void Write(string message = "", LogType logType = LogType.Information, LogLevel logLevel = LogLevel.One)
    {
        foreach (var logger in Loggers)
            logger.Write(message, logType, logLevel);
    }

    public static void Write(LogLines lines)
    {
        foreach (var logger in Loggers)
            logger.Write(lines);
    }
    
    public static void Write(Exception exception, LogType logType = LogType.Error, LogLevel logLevel = LogLevel.One)
    {
        LogLines lines = [];

        int indentLenth = 0;
        Exception? ex = exception;
        while (ex != null)
        {
            var indent = new string(' ', indentLenth);
            lines.Add($"{indent}Type: {ex.GetType().FullName}", logType, logLevel);
            lines.Add($"{indent}Message: {ex.Message}", logType, logLevel);
            
            if (ex.StackTrace != null)
            {
                foreach (var stackLine in ex.StackTrace.Split(Environment.NewLine))
                    lines.Add($"{indent}StackTrace: {stackLine.Trim()}", logType, logLevel);
            }

            indentLenth += 2;
            ex = ex.InnerException;
        }
    }
}