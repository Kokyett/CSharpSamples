using CSharpSamples.Services.Counters;
using CSharpSamples.Services.Logging;

namespace CSharpSamples.Samples.Streams;

public class ReadWriteMemoryStreamSample : Sample
{
    private readonly ReadWriteMemoryStream _stream = new(1000);
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    protected override void Run()
    {
        var thread1 = new Thread(() =>
        {
            using StreamWriter sw = new(_stream);
            sw.AutoFlush = true;
            for (int i = 0; i < 10; i++)
            {
                string line = $"Line {i}"; 
                Log.Write($"Thread1: write {line}");
                sw.WriteLine(line);
                Counters.Add("Written lines", CounterType.Integer, LogType.Information, 1);
                Thread.Sleep(i * 500);
            }
            _cancellationTokenSource.Cancel();
        });

        var thread2 = new Thread(() =>
        {
            using StreamReader sr = new(_stream);
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    Log.Write($"Thread2: read {sr.ReadLine()}");
                    Counters.Add("Read lines", CounterType.Integer, LogType.Information, 1);
                }
                catch (TimeoutException)
                {
                    Log.Write("Thread2: TimeoutException");
                    Counters.Add("Timeout exception", CounterType.Integer, LogType.Warning, 1);
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
            }
        });
        thread1.Start();
        thread2.Start();
        
        thread1.Join();
        thread2.Join();
    }
    
    protected override void Terminate()
    {
        _stream.Dispose();
        base.Terminate();
    }
}