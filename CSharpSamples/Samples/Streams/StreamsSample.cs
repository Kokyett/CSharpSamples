using CSharpSamples.Utils;

namespace CSharpSamples.Samples.Streams;

public class StreamsSample : Sample
{
    public override async Task Run()
    {
        await base.Run();
        await using ReadWriteMemoryStream ms = new(true, 5000);
        using StreamReader sr = new(ms);
        await using StreamWriter sw = new(ms);
        sw.AutoFlush = true;

        Log.WriteLine("Write into ReadWriteMemoryStream");
        await sw.WriteLineAsync("Hello World !");

        Log.WriteLine("Read ReadWriteMemoryStream");
        string? line = await sr.ReadLineAsync();
        Log.WriteLine($"  - {line}");

        Log.WriteLine($"Read from ReadWriteMemoryStream with {ms.ReadTimeout} ms timeout");
        line = await sr.ReadLineAsync();
        Log.WriteLine($"  - {line ?? "NULL"}");
    }
}