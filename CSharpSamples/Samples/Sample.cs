using CSharpSamples.Utils;

namespace CSharpSamples.Samples;

public abstract class Sample
{
    protected Counters Counters { get; } = new();
    
    public virtual async Task Initialize()
    {
        await Task.CompletedTask;
        string title = $"Sample type: {GetType().FullName}";
        string separator = new string('-', title.Length);
        Log.WriteLine(separator, LogType.Highlight);
        Log.WriteLine(title, LogType.Highlight);
        Log.WriteLine(separator, LogType.Highlight);
    }

    public virtual async Task Run()
    {
        await Task.CompletedTask;
    }
    
    public virtual async Task Finish()
    {
        await Task.CompletedTask;
        Counters.WriteToLog();
    }
}