
Span<int> left = stackalloc int[1000];
Span<int> right = stackalloc int[1000];

var lines = File.ReadAllLines("Input.txt");
for (int i = 0; i < lines.Length; i++)
{
    var sides = lines[i].Split("   ");
    left[i] = int.Parse(sides[0]);
    right[i] = int.Parse(sides[1]);
}

left.Sort();
right.Sort();

int total = 0;
for (int i = 0; i < left.Length; i++)
    total += Math.Abs(left[i] - right[i]);
    
Console.WriteLine($"Part 1: {total}");

var cursor = 0;
var similarityScore = 0;

foreach (var current in left)
{
    var count = 0;
    for (int c = cursor; c < right.Length; c++)
    {
        var rightSide = right[c];
        if (rightSide == current)
        {
            count++;
        }
        else if (rightSide > current)
        {
            // save cursor to make code go faster or something idk
            cursor = c;
            break;
        }
    }
    similarityScore += current * count;
}

Console.WriteLine($"Part 2: {similarityScore}");