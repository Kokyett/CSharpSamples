using CSharpSamples.Services.Logging;

namespace CSharpSamples.Samples.DependencyInjection;

public class ServicesSample : Sample
{
    private readonly Services _services = new();

    protected override void Run()
    {
        Log.Write($"Get service {nameof(Test3)}");
        _services.Get<Test3>();
        Log.Write();

        Log.Write($"Get service {nameof(Test3)}");
        _services.Get<Test3>();
        Log.Write();

        Log.Write($"Get service {nameof(Test3)}");
        _services.Get<Test3>();
    }
}