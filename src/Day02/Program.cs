int safeReports = 0;
foreach (var line in File.ReadAllLines("Input.txt"))
{
    Directionality? directionality = null;
    var samples = line.Split(" ").Select(int.Parse).ToArray();
    for (int i = 0; i < samples.Length - 1; i++)
    {
        var firstSample = samples[i];
        var secondSample = samples[i + 1];
        
        // are numbers same
        if (firstSample == secondSample)
            break;

        var difference = firstSample - secondSample;
        var localizedDirectionality = difference > 0 ? Directionality.Decreasing : Directionality.Increasing;
        directionality ??= localizedDirectionality;

        // at most 3
        if (difference is > 3 or < -3)
            break;
        
        // end of report
        if (samples.Length == i + 2)
            safeReports++;
    }
}

Console.WriteLine($"Part 1: {safeReports}");

enum Directionality
{
    Increasing,
    Decreasing
}