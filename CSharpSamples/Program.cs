using System.Reflection;
using CSharpSamples.Samples;
using CSharpSamples.Utils;

foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
{
    if (!type.IsAbstract && type.IsAssignableTo(typeof(Sample)))
    {
        try
        {
            Sample sample = (Sample)(Activator.CreateInstance(type) ?? throw new NullReferenceException());
            await sample.Initialize();
            await sample.Run();
            await sample.Finish();
            Log.WriteLine(string.Empty);
            Log.WriteLine(string.Empty);
            Log.WriteLine(string.Empty);
        }
        catch (Exception e)
        {
            Log.WriteLine(e);
            Log.WriteLine(string.Empty);
            Log.WriteLine(string.Empty);
            Log.WriteLine(string.Empty);
        }
    }
}