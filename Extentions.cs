using System.Text.RegularExpressions;

namespace HangfireRecuringJob;

internal static partial class Extensions
{
    private static readonly Regex regex = new("[^a-zA-Z0-9]+", RegexOptions.Compiled);
    internal static string Sanitize(this string input, string replacement = "_") => regex.Replace(input, replacement);

}