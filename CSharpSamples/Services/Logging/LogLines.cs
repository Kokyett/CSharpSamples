using System.Collections;

namespace CSharpSamples.Services.Logging;

public class LogLines: IEnumerable<LogLine>
{
    private readonly List<LogLine> _logLines = [];

    public void Add(string message, LogType logType, LogLevel logLevel)
    {
        _logLines.Add(new LogLine
        {
            Message = message,
            LogType = logType,
            LogLevel = logLevel
        });
    }
    
    public IEnumerator<LogLine> GetEnumerator()
    {
        return _logLines.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _logLines.GetEnumerator();
    }
}