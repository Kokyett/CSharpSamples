namespace CSharpSamples.Samples.DependencyInjection;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute<T> : Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class SingletonServiceAttribute<T> : Attribute;
