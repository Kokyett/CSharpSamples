namespace CSharpSamples.Services.Logging;

public class LogLine
{
    public required string Message { get; init; }
    public required LogType LogType { get; init; }
    public required LogLevel LogLevel { get; init; }
}