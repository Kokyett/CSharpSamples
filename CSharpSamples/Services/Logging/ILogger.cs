namespace CSharpSamples.Services.Logging;

public interface ILogger : IDisposable
{
    LogLevel LogLevel { get; }
    void Write(string message, LogType logType, LogLevel logLevel);
    void Write(LogLines lines);
}