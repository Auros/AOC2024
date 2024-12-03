using System.Text.RegularExpressions;

namespace Day03;

public partial struct MullRegex
{
    private const string MullRegexString = @"(mul\((?<First>([0-9]+))*,(?<Second>([0-9]+))*\))";
    private const string MullDoDontRegexString = @"((?<Key>[a-z']+)\(\))|(mul\((?<First>([0-9]+))*,(?<Second>([0-9]+))*\))";

    [GeneratedRegex(MullRegexString, RegexOptions.Multiline)]
    public static partial Regex Mull();
    
    [GeneratedRegex(MullDoDontRegexString, RegexOptions.Multiline)]
    public static partial Regex MullDoDont();
}