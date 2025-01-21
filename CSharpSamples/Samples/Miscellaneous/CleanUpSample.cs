using CSharpSamples.Services;
using CSharpSamples.Services.Counters;
using CSharpSamples.Services.Logging;

namespace CSharpSamples.Samples.Miscellaneous;

public class CleanUpSample : Sample
{
    protected override void Run()
    {
        Log.Write($"Clean up {Application.LogDirectory.FullName}");
        CleanUpDirectory(Application.LogDirectory, new(1, 0, 0), false);
        Log.Write();
        Log.Write($"Clean up {Application.TempDirectory.FullName}");
        CleanUpDirectory(Application.TempDirectory, new(30, 0, 0, 0), false);
        Log.Write();
    }

    private void CleanUpDirectory(DirectoryInfo di, TimeSpan timeLimit, bool deleteDirectory)
    {
        foreach (DirectoryInfo sdi in di.GetDirectories())
        {
            Log.Write($"  - Clean up directory {sdi.FullName}");
            CleanUpDirectory(sdi, timeLimit, true);
        }

        Log.Write($"  - Check {di.FullName} files");
        foreach (FileInfo fi in di.GetFiles())
        {
            Log.Write($"    - Check file {fi.Name}");
            Counters.Add("Checked files", CounterType.Integer, LogType.Information, 1);
            if (DateTime.Now - fi.LastWriteTime > timeLimit)
            {
                Log.Write($"      - Last write rime {fi.LastWriteTime}");
                Log.Write("      - Delete file");
                Counters.Add("Deleted files", CounterType.Integer, LogType.Information, 1);
                fi.Delete();
            }
        }

        if (deleteDirectory && di.GetDirectories().Length == 0 && di.GetFiles().Length == 0)
        {
            Log.Write($"  - Delete directory {di.FullName}");
            Counters.Add("Deleted directories", CounterType.Integer, LogType.Information, 1);
            di.Delete();
        }
    }
}
