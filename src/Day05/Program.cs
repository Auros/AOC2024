var data = File.ReadAllLines("Input.txt");

int updatesIndex = 0;
Span<int> countTracker = stackalloc int[100];
Span<int> rules = stackalloc int[10000];

for (int i = 0; i < data.Length; i++)
{
    var line = data[i];
    if (string.IsNullOrEmpty(line))
    {
        // stop once we reach the break separating the input data sections
        updatesIndex = i + 1;
        break;
    }

    // load the page rule
    var lineSpan = line.AsSpan();
    var p1 = int.Parse(lineSpan[..2]);
    var p2 = int.Parse(lineSpan[3..]);

    var storageIndex = countTracker[p1]++;
    rules[p1 * 100 + storageIndex] = p2;
}

int correctMiddlePageSums = 0;
int correctedMiddlePageSums = 0;

Span<int> updateBuffer = stackalloc int[100];
for (int i = updatesIndex; i < data.Length; i++)
{
    int updateLength = 0;
    var line = data[i].AsSpan();

    // parsing update line into the buffer
    foreach (var range in line.Split(','))
    {
        updateBuffer[updateLength++] = int.Parse(line[range]);
    }

    bool IsValid(ReadOnlySpan<int> updateBuffer, ReadOnlySpan<int> countTracker, ReadOnlySpan<int> rules)
    {
        bool isValid = true;

        // Looping over each element in the current updates page line
        for (int c = 0; c < updateLength && isValid; c++)
        {
            var current = updateBuffer[c];
            var valueSize = countTracker[current];
            var valueOffset = current * 100;

            // this element does not have any rules!
            if (valueSize == 0)
                continue;

            // if anything in the page rules is BEFORE the current index
            // then its definitely invalid
            var region = rules.Slice(valueOffset, valueSize);
            if (updateBuffer[..c].ContainsAny(region))
            {
                isValid = false;
                break;
            }

            for (int d = c + 1; d < updateLength; d++)
            {
                var next = updateBuffer[d];
                if (region.Contains(next))
                    continue;

                // break out if this rule was not satisfied
                isValid = false;
                break;
            }
        }

        return isValid;
    }

    if (IsValid(updateBuffer, countTracker, rules))
    {
        correctMiddlePageSums += updateBuffer[updateLength / 2]; // Grab middle number and sum
        continue;
    }

    // handle incorrects
    for (int c = 0; c < updateLength; c++)
    {
        var current = updateBuffer[c];
        var valueSize = countTracker[current];
        var valueOffset = current * 100;

        // this element does not have any rules!
        if (valueSize == 0)
            continue;

        // if NOTHING in the page rules is BEFORE the current index
        // then this number is valid, and we don't need to check further
        var region = rules.Slice(valueOffset, valueSize);
        if (!updateBuffer[..c].ContainsAny(region))
            continue;

        for (int d = c - 1; d >= 0; d--)
        {
            var target = updateBuffer[d];

            if (region.IndexOf(target) is -1)
                continue;

            for (int q = c; q > d; q--)
                updateBuffer[q] = updateBuffer[q - 1];

            // insert new index
            updateBuffer[d] = current;

            // reset search back to safe value to evaluate new state
            c = d - 1;

            break;
        }

        if (IsValid(updateBuffer, countTracker, rules))
        {
            correctedMiddlePageSums += updateBuffer[updateLength / 2];
            break;
        }
    }
}

Console.WriteLine("Part 1: " + correctMiddlePageSums);
Console.WriteLine("Part 2: " + correctedMiddlePageSums);