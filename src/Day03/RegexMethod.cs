using System.Text.RegularExpressions;

namespace Day03;

public class RegexMethod
{
    public static void Run(string data)
    {
        var result = MullRegex.Mull();

        var added = 0;
        foreach (Match match in result.Matches(data))
        {
            var num1 = int.Parse(match.Groups["First"].ValueSpan);
            var num2 = int.Parse(match.Groups["Second"].ValueSpan);
            added += num1 * num2;
        }

        Console.WriteLine($"(Regex) Part 1: {added}");

        added = 0;
        bool canMultiply = true;
        result = MullRegex.MullDoDont();

        foreach (Match match in result.Matches(data))
        {
            var keyGroup = match.Groups["Key"];
            if (keyGroup.Success)
            {
                canMultiply = keyGroup.Value switch
                {
                    "do" => true,
                    "don't" => false,
                    _ => canMultiply // set it back to itself if it's anything else
                };
            }
            else if (canMultiply)
            {
                var num1 = int.Parse(match.Groups["First"].ValueSpan);
                var num2 = int.Parse(match.Groups["Second"].ValueSpan);
                added += num1 * num2;
            }
        }

        Console.WriteLine($"(Regex) Part 2: {added}");
    }
}