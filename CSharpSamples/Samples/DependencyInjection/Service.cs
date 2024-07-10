namespace CSharpSamples.Samples.DependencyInjection;

internal interface IService
{
    object GetInstance();
}

internal class Service : IService
{
    private Func<object> _func;
    public Service(object func)
    {
        _func = (Func<object>)func;
    }
    public object GetInstance()
    {
        return _func.Invoke();
    }
}

internal class SingletonService : IService
{
    private Lazy<object> _lazy;
    public SingletonService(object func)
    {
        _lazy = new((Func<object>)func);
    }
    public object GetInstance()
    {
        return _lazy.Value;
    }
}