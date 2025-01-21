using CSharpSamples.Services.Logging;

namespace CSharpSamples.Samples.DependencyInjection;

[SingletonService<Test1>]
public class Test1
{
    public Test1()
    {
        Log.Write($"  - {nameof(Test1)} constructor");
    }
}

[Service<Test2>]
public class Test2
{
    public Test2()
    {
        Log.Write($"  - {nameof(Test2)} constructor");
    }
}

[Service<Test3>]
public class Test3
{
    public Test3(Test1 test1, Test2 test2)
    {
        Log.Write($"  - {nameof(Test3)} constructor");
    }
}