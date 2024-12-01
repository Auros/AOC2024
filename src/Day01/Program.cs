
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
    
Console.WriteLine(total);

// I'll do part 2 later