using System.Reflection;

namespace CSharpSamples.Samples.DependencyInjection;

public class ServiceProvider
{
    private readonly Dictionary<Type, IService> _services = new();

    public void Register<T>() where T : class
    {
        Register(CreateObject<T>);
    }
    public void Register<T, TClass>() where TClass : T
    {
        Register<T>(() => CreateObject<TClass>());
    }
    public void Register<T>(Func<T> func)
    {
        _services.Add(typeof(T), new Service(func));
    }
    
    public void RegisterSingleton<T>() where T : class
    {
        RegisterSingleton(CreateObject<T>);
    }
    public void RegisterSingleton<T, TClass>() where TClass : T
    {
        RegisterSingleton<T>(() => CreateObject<TClass>());
    }
    public void RegisterSingleton<T>(Func<T> func)
    {
        _services.Add(typeof(T), new SingletonService(func));
    }
    
    public object GetRequiredService(Type type)
    {
        return _services[type].GetInstance();
    }    
    public T GetRequiredService<T>()
    {
        return (T)GetRequiredService(typeof(T));
    }    
    
    private T CreateObject<T>()
    {
        return (T)CreateObject(typeof(T));
    }
    
    private object CreateObject(Type type)
    {
        ConstructorInfo constructor = type.GetConstructors()[0];
        ParameterInfo[] parameters = constructor.GetParameters();
        object? instance = parameters.Length == 0
            ? Activator.CreateInstance(type)
            : Activator.CreateInstance(type, parameters.Select(pi => GetRequiredService(pi.ParameterType)));
        return instance ?? throw new NullReferenceException();
    }
}