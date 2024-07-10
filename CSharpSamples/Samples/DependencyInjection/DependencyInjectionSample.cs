using CSharpSamples.Utils;

namespace CSharpSamples.Samples.DependencyInjection;

public class DependencyInjectionSample : Sample
{
    public override async Task Run()
    {
        await base.Run();
        ServiceProvider provider = new();
        provider.Register<TestService>();
        provider.RegisterSingleton<TestSingletonService>();

        for (int i = 0; i < 3; i++)
        {
            provider.GetRequiredService<TestService>();
            provider.GetRequiredService<TestSingletonService>();
        }
    }

    private class TestService
    {
        public TestService()
        {
            Log.WriteLine($"Constructor: {GetType().FullName}");    
        }
    }

    private class TestSingletonService
    {
        public TestSingletonService()
        {
            Log.WriteLine($"Constructor: {GetType().FullName}");    
        }
    }
}