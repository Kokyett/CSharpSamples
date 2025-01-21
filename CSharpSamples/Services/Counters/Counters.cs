using System.Collections.Concurrent;
using CSharpSamples.Services.Logging;

namespace CSharpSamples.Services.Counters;

public class Counters
{
    private readonly ConcurrentDictionary<Counter, Counter> _dictionary = [];

    public void Add(string label, CounterType counterType, LogType logType, long value)
    {
        var counter = new Counter()
        {
            Label = label,
            CounterType = counterType,
            LogType = logType
        };
        _dictionary.GetOrAdd(counter, counter).Add(value);
    }

    public LogLines GetLogLines()
    {
        var logLines = new LogLines();
        if (_dictionary.Count == 0)
            return logLines;
        
        var maxLabelLength = Math.Max(20, _dictionary.Values.Max(x => x.Label.Length) + 3);
        var maxValueLength = Math.Max(5, _dictionary.Values.Max(x => x.FormattedValue.Length));
        foreach (var counter in _dictionary.Keys.OrderBy(x => x.Id))
            logLines.Add($"{counter.Label.PadRight(maxLabelLength, '.')}{counter.FormattedValue.PadLeft(maxValueLength, '.')}", counter.LogType, LogLevel.One);
        return logLines;
    }
}