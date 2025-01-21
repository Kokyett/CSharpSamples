using CSharpSamples.Extensions;
using CSharpSamples.Services.Logging;

namespace CSharpSamples.Services.Counters;

public class Counter
{
    private static int _id = 0;
    public int Id { get; } = Interlocked.Increment(ref _id);
    
    public required string Label { get; init; }
    public required CounterType CounterType { get; init; }
    public LogType LogType { get; init; }

    private long _value = 0;
    public long Value => _value;

    public string FormattedValue
    {
        get => CounterType switch
        {
            CounterType.ByteSize => _value.ToReadableString("F2"),
            CounterType.Duration => TimeSpan.FromMilliseconds(_value).ToString("g"),
            _ => _value.ToString()
        };
    }
    
    public void Add(long value)
    {
        Interlocked.Add(ref _value, value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Counter counter)
            return false;
        return counter.Label == Label
               && counter.CounterType == CounterType
               && counter.LogType == LogType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Label, CounterType, LogType);
    }
}