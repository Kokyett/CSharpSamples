using System.Diagnostics.CodeAnalysis;

namespace CSharpSamples.Extensions;

public static class IntegerExtensions
{
    private static readonly string[] Metrics = [ "B", "KB", "MB", "GB", "TB" ];
    
    public static string ToReadableString(this int number, [StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format = null)
    {
        int index = (int)Math.Log(number, 1000);
        if (index >= Metrics.Length)
            index  = Metrics.Length - 1;
        return $"{(number / Math.Pow(1000, index)).ToString(format)} {Metrics[index]}";
    }

    public static string ToReadableString(this long number, [StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format = null)
    {
        int index = (int)Math.Log(number, 1000);
        if (index >= Metrics.Length)
            index  = Metrics.Length - 1;
        return $"{(number / Math.Pow(1000, index)).ToString(format)} {Metrics[index]}";
    }
}