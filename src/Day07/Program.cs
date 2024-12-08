var data = File.ReadAllLines("Input.txt");

long part1Sum = 0;
long part2Sum = 0;
Span<int> buffer = stackalloc int[64];

foreach (var line in data)
{
    var ls = line.AsSpan();
    var separatorIndex = ls.IndexOf(':');
    var answer = long.Parse(ls[..separatorIndex]);
    int elementCount = 0;

    var input = ls[(separatorIndex + 2)..];
    var elements = input.Split(' ');
    while (elements.MoveNext())
    {
        var elementSpan = input[elements.Current];
        buffer[elementCount++] = int.Parse(elementSpan);
    }

    // handle only addition and multiplication stuffs (really just used to calculate part 1 answer)
    if (IsBranchSafe(answer, buffer[..elementCount], buffer[0], 1, Operation.Addition))
    {
        part1Sum += answer;
        part2Sum += answer;
    }
    else if (IsBranchSafe(answer, buffer[..elementCount], buffer[0], 1, Operation.Multiplication))
    {
        part1Sum += answer;
        part2Sum += answer;
    }
    else
    {
        // calculate problems that might need concatenation using inverse lookup
        if (IsBranchSafeInverse(buffer[0], buffer[..elementCount], answer, elementCount - 1, Operation.Addition))
        {
            part2Sum += answer;
        }
        else if (IsBranchSafeInverse(buffer[0], buffer[..elementCount], answer, elementCount - 1, Operation.Multiplication))
        {
            part2Sum += answer;
        }
        else if (IsBranchSafeInverse(buffer[0], buffer[..elementCount], answer, elementCount - 1, Operation.Concatenation))
        {
            part2Sum += answer;
        }
    }
}


Console.WriteLine("Part 1: " + part1Sum);
Console.WriteLine("Part 2: " + part2Sum);

bool IsBranchSafe(long goal, ReadOnlySpan<int> options, long previous, int startIndex, Operation operation)
{
    for (int i = startIndex; i < options.Length; i++)
    {
        var value = options[i];
        previous = operation switch
        {
            Operation.Addition => previous + value,
            Operation.Multiplication => previous * value,
            Operation.Concatenation or _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };

        // calculated is greater than goal, no need to waste time over something we already know.
        if (previous > goal)
            return false;

        if (IsBranchSafe(goal, options, previous, i + 1, Operation.Addition))
        {
            return true;
        }

        if (IsBranchSafe(goal, options, previous, i + 1, Operation.Multiplication))
        {
            return true;
        }
    }

    return goal == previous;
}

bool IsBranchSafeInverse(int goal, ReadOnlySpan<int> options, long working, int startIndex, Operation operation)
{
    if (startIndex > 0)
    {
        long concatenated = 0;
        var value = options[startIndex];

        // invalid branch
        if (operation is Operation.Multiplication && working % value != 0)
            return false;

        // invalid branch
        if (operation is Operation.Concatenation && !TrimFromLong(working, value, out concatenated))
            return false;

        var previous = operation switch
        {
            Operation.Addition => working - value,
            Operation.Multiplication => working / value,
            Operation.Concatenation => concatenated,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
        };

        // invalid branch
        if (previous < 0)
            return false;

        if (IsBranchSafeInverse(goal, options, previous, startIndex - 1, Operation.Multiplication))
        {
            //Console.WriteLine(operation);
            return true;
        }
        if (IsBranchSafeInverse(goal, options, previous, startIndex - 1, Operation.Addition))
        {
            //Console.WriteLine(operation);
            return true;
        }
        if (IsBranchSafeInverse(goal, options, previous, startIndex - 1, Operation.Concatenation))
        {
            //Console.WriteLine(operation);
            return true;
        }
    }
    return startIndex == 0 && goal == working;
}

bool TrimFromLong(long subject, int trim, out long value)
{
    value = 0;
    Span<char> trimBufferA = stackalloc char[128];
    Span<char> trimBufferB = stackalloc char[64];

    subject.TryFormat(trimBufferA, out var aBufferSize);
    trim.TryFormat(trimBufferB, out var bBufferSize);

    if (bBufferSize >= aBufferSize)
        return false;

    value = long.Parse(trimBufferA[..(aBufferSize - bBufferSize)]);
    return trimBufferA.Slice(aBufferSize - bBufferSize, bBufferSize).SequenceEqual(trimBufferB[..bBufferSize]);
}

internal enum Operation
{
    Addition,
    Multiplication,
    Concatenation
}