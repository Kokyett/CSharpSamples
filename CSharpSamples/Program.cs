using CSharpSamples.Samples;
using CSharpSamples.Samples.DependencyInjection;
using CSharpSamples.Samples.Miscellaneous;
using CSharpSamples.Samples.Streams;
using CSharpSamples.Services.Logging;

using ConsoleLogger consoleLogger = new(LogLevel.One);
Log.RegisterLogger(consoleLogger);

using HtmlLogger htmlLogger = new(LogLevel.Three);
Log.RegisterLogger(htmlLogger);

Sample.Run<CleanUpSample>();
Sample.Run<ReadWriteMemoryStreamSample>();
Sample.Run<ServicesSample>();