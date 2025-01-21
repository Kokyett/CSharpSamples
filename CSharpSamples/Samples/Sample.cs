using CSharpSamples.Services.Counters;
using CSharpSamples.Services.Logging;

namespace CSharpSamples.Samples;

public abstract class Sample
{
    protected readonly Counters Counters;

    protected Sample()
    {
        Counters = new Counters();
    }

    private void LogHeader()
    {
        var name = GetType().FullName ?? "Unknown type";
        var separator = new string('=', name.Length);
        Log.Write(separator, LogType.Important);
        Log.Write(name, LogType.Important );    
        Log.Write(separator, LogType.Important);
    }

    protected virtual void Initialize()
    {
        
    }
    
    protected abstract void Run();

    protected virtual void Terminate()
    {
        var lines = Counters.GetLogLines();
        if (lines.Any())
        {
            var separator = new string('-', lines.Max(x => x.Message.Length));
            Log.Write(separator);
            Log.Write(lines);
            Log.Write(separator);
        }
        Log.Write();
        Log.Write();
    }

    public static void Run<T>() where T : Sample
    {
        var instance = Activator.CreateInstance<T>();
        instance.LogHeader();
        instance.Initialize();
        instance.Run();
        instance.Terminate();
    } 
}