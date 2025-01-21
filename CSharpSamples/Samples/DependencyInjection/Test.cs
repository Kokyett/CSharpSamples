using CSharpSamples.Services.Logging;

namespace CSharpSamples.Samples.DependencyInjection;

public class Test1
{
    public Test1()
    {
        Log.Write($"  - {nameof(Test1)} constructor");
    }
}

public class Test2
{
    public Test2()
    {
        Log.Write($"  - {nameof(Test2)} constructor");
    }
}

public class Test3
{
    public Test3(Test1 test1, Test2 test2)
    {
        Log.Write($"  - {nameof(Test3)} constructor");
    }
}