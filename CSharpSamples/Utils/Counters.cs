namespace CSharpSamples.Utils;

public class Counters : HashSet<Counter>
{
    public void Add(string code, string label, LogType type, int count = 1)
    {
        Counter compare = new()
        {
            Code = code,
            Label = label,
            Type = type,
            Count = count
        };
        if (TryGetValue(compare, out Counter? counter))
            counter.Count += count;
        else
            Add(compare);
    }

    public void WriteToLog()
    {
        if (Count == 0)
            return;

        int maxCode = Math.Max(4, this.Max(x => x.Code.Length));
        int maxLabel = Math.Max(5, this.Max(x => x.Label.Length));
        int maxCount = Math.Max(5, this.Max(x => x.Count.ToString().Length));

        string separator = new string('-', maxCode + 3 + maxLabel + 3 + maxCount);
        Log.WriteLine(separator);
        Log.Write("Code".PadRight(maxCode + 3, ' '));
        Log.Write("Label".PadRight(maxLabel + 3, ' '));
        Log.WriteLine("Count".PadRight(maxCount, ' '));
        Log.WriteLine(separator);
        foreach (var counter in this)
        {
            string line = counter.Code.PadRight(maxCode + 3, ' ') + 
                          counter.Label.PadRight(maxLabel + 3, ' ') +
                          counter.Count.ToString().PadLeft(maxCount, ' ');
            Log.WriteLine(line, counter.Type);
        }
        Log.WriteLine(separator);
    }
}

public class Counter
{
    public required string Code { get; init; }
    public required string Label { get; init; }
    public required LogType Type { get; init; }
    public int Count { get; set;  }

    public override bool Equals(object? obj)
    {
        return obj is Counter counter &&
               Code.Equals(counter.Code) &&
               Label.Equals(counter.Label) &&
               Type.Equals(counter.Type);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Label, Type);
    }
}