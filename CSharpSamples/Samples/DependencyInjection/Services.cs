using System.Reflection;

namespace CSharpSamples.Samples.DependencyInjection;

public class Services
{
    private readonly Dictionary<Type, IService> _services = [];

    #region Get service
    public T Get<T>()
    {
        return (T)Get(typeof(T));
    }
    public object Get(Type type)
    {
        if (!_services.TryGetValue(type, out IService? service))
            throw new Exception($"Unregistered service {type.FullName}");
        return service.GetInstance();
    }
    #endregion
    
    #region Register service
    private void Add(Type type, IService service)
    {
        if (!_services.TryAdd(type, service))
            throw new Exception($"Already registered service {type.FullName}");
    }
    public void RegisterConstant<T>(T instance) where T : notnull
    {
        Add(typeof(T), new ConstantService<T>(instance));
    }
    public void RegisterSingleton<T>(Func<T> factory) where T : notnull
    {
        Add(typeof(T), new SingletonService<T>(factory));
    }
    public void RegisterSingleton<T>() where T : notnull
    {
        RegisterSingleton(CreateInstance<T>);
    }
    public void RegisterSingleton<TBase, T>() where T : TBase where TBase : notnull
    {
        // ReSharper disable once ConvertClosureToMethodGroup
        RegisterSingleton<TBase>(() => CreateInstance<T>());
    }
    public void Register<T>(Func<T> factory) where T : notnull
    {
        Add(typeof(T), new Service<T>(factory));
    }
    public void Register<T>() where T : notnull
    {
        Register(CreateInstance<T>);
    }
    public void Register<TBase, T>() where T : TBase where TBase : notnull
    {
        // ReSharper disable once ConvertClosureToMethodGroup
        Register<TBase>(() => CreateInstance<T>());
    }
    private T CreateInstance<T>() where T : notnull
    {
        Type type = typeof(T);
        ConstructorInfo[] ctors = type.GetConstructors();
        if (ctors.Length != 1)
            throw new Exception($"Constructor {type.FullName} must have exactly one public constructor");
        
        ConstructorInfo ctor = ctors[0];
        object[] args = ctor.GetParameters().Select(p => Get(p.ParameterType)).ToArray();
        return (T)ctor.Invoke(args.Length > 0 ? args : null);
    }
    #endregion
    
    #region Subclasses
    interface IService
    {
        object GetInstance();
    }
    class ConstantService<T>(T instance) : IService where T : notnull
    {
        public object GetInstance()
        {
            return instance;
        }
    }
    class SingletonService<T>(Func<T> factory) : IService where T : notnull
    {
        private readonly Lazy<T> _lazy = new(factory);

        public object GetInstance()
        {
            return _lazy.Value;
        }
    }
    class Service<T>(Func<T> factory) : IService where T : notnull
    {
        public object GetInstance()
        {
            return factory();
        }
    }
    #endregion
}