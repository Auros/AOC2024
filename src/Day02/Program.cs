var safeReports = 0;
var tolerantReports = 0;

foreach (var line in File.ReadAllLines("Input.txt"))
{
    var samples = line.Split(" ").Select(int.Parse).ToArray();
    var result = IsSafe(samples, true);

    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (result is SafetyReport.Safe)
        safeReports++;
    else if (result is SafetyReport.Tolerant)
        tolerantReports++;
}

Console.WriteLine($"Part 1: {safeReports}");
Console.WriteLine($"Part 2: {safeReports + tolerantReports}");

return;

unsafe SafetyReport IsSafe(Span<int> data, bool recursive = false)
{
    // If the first two numbers are the same, just drop the first number (doesn't make a difference)
    if (data[0] == data[1])
    {
        // ReSharper disable once TailRecursiveCall
        return recursive ? IsSafe(data[1..]) : SafetyReport.Unsafe;
    }
    
    Directionality? directionality = null;
    for (var i = 0; i < data.Length - 1; i++)
    {
        var first = data[i];
        var second = data[i + 1];

        var difference = first - second;
        var localizedDirectionality = difference > 0 ? Directionality.Decreasing : Directionality.Increasing;
        directionality ??= localizedDirectionality; // difference == 0 handled at start of IsSafe

        // check conditions
        // ReSharper disable once InvertIf
        if (first == second || directionality != localizedDirectionality || difference is > 3 or < -3)
        {
            if (!recursive) // we're already in a failing branch, no point checking further
                return SafetyReport.Unsafe;

            // ReSharper disable once StackAllocInsideLoop
            Span<int> localSamples = stackalloc int[data.Length - 1]; // freaky

            // check both branches of removing either number
            var branch = IsSafe(RemoveIndex(data, localSamples, i));
            if (branch is SafetyReport.Unsafe) // first branch failed, check other future
                branch = IsSafe(RemoveIndex(data, localSamples, i + 1));
            if (branch is SafetyReport.Unsafe && i != 0) // sometimes previous can be culprit
                branch = IsSafe(RemoveIndex(data, localSamples, i - 1));

            return branch;
        }
    }

    return recursive ? SafetyReport.Safe : SafetyReport.Tolerant;
}

Span<int> RemoveIndex(ReadOnlySpan<int> data, Span<int> destination, int index)
{
    // removes the specified index from the span "data" and writes it to "destination"
    // then returns that destination to make my code look prettier

    var written = 0;
    var firstHalf = data[..index];
    var secondHalf = data[(index + 1)..];

    foreach (var sample in firstHalf)
        destination[written++] = sample;
    foreach (var sample in secondHalf)
        destination[written++] = sample;

    return destination;
}

internal enum Directionality
{
    Increasing,
    Decreasing
}

internal enum SafetyReport
{
    Unsafe,
    Safe,
    Tolerant
}