var data = File.ReadAllText("Input.txt").AsSpan();

var width = data.IndexOf('\n') - 1; // look for the first newline
var height = data.Count('\n') + 1; // count how many newlines + 1 for the last line

int xmasCount = 0;
int xmasCountPart2 = 0;
int incrementer = 0;

// load the thingies without the newlines
Span<Stage> stages = stackalloc Stage[width * height];
foreach (var symbol in data)
{
    Stage? stage = symbol switch
    {
        'X' => Stage.X,
        'M' => Stage.M,
        'A' => Stage.A,
        'S' => Stage.S,
        _ => null
    };

    if (!stage.HasValue)
        continue;
    
    stages[incrementer++] = stage.Value;
}

// these are the directions we need to scan in
Span<Vector2Int> directions = stackalloc Vector2Int[8];
directions[0] = new Vector2Int(-1, -1);
directions[1] = new Vector2Int(-1, 0);
directions[2] = new Vector2Int(-1, 1);
directions[3] = new Vector2Int(0, -1);
directions[4] = new Vector2Int(0, 1);
directions[5] = new Vector2Int(1, -1);
directions[6] = new Vector2Int(1, 0);
directions[7] = new Vector2Int(1, 1);

for (int i = 0; i < stages.Length; i++)
{
    if (stages[i] is Stage.X)
    {
        // Start looking at X and recursively scan neighbors
        foreach (var direction in directions)
            if (Searcher.IsValid(width, height, stages, i, Stage.X, direction))
                xmasCount++;
    }
    else if (stages[i] is Stage.A)
    {
        var rowIndex = i % width;
        var columnIndex = i / width;

        // no point in checking stuff on the edges
        if (rowIndex == 0 || rowIndex == width - 1 || columnIndex == 0 || columnIndex == height - 1)
            continue;
        
        // Look for the X pattern
        int validCorners = 0;
        
        // top left
        var corner = i - 1 - width;
        if (stages[corner] is Stage.M && Searcher.IsValid(width, height, stages, corner, Stage.M, new Vector2Int(1, 1)))
            validCorners++;
        
        // top right
        corner = i + 1 - width;
        if (stages[corner] is Stage.M && Searcher.IsValid(width, height, stages, corner, Stage.M, new Vector2Int(-1, 1)))
            validCorners++;

        // bottom left
        corner = i - 1 + width;
        if (stages[corner] is Stage.M && Searcher.IsValid(width, height, stages, corner, Stage.M, new Vector2Int(1, -1)))
            validCorners++;
        
        // bottom right
        corner = i + 1 + width;
        if (stages[corner] is Stage.M && Searcher.IsValid(width, height, stages, corner, Stage.M, new Vector2Int(-1, -1)))
            validCorners++;

        if (validCorners == 2)
            xmasCountPart2++;
    }
}

Console.WriteLine("Part 1: " + xmasCount);
Console.WriteLine("Part 2: " + xmasCountPart2);

internal enum Stage
{
    X,
    M,
    A,
    S
}

internal record struct Vector2Int(int X, int Y);

internal class Searcher
{
    public static bool IsValid(int width, int height, ReadOnlySpan<Stage> stages, int index, Stage stage, Vector2Int direction)
    {
        var rowIndex = index % width;
        var columnIndex = index / width;
    
        var nextRowIndex = rowIndex + direction.X;
        var nextColumnIndex = columnIndex + direction.Y;

        // out of bounds
        if (0 > nextRowIndex || nextRowIndex >= width || 0 > nextColumnIndex || nextColumnIndex >= height)
            return false;
    
        var nextIndex = index + direction.X + direction.Y * width;
        var nextStage = stages[nextIndex]; // Find the next character in the word based on the direction

        return (stage is Stage.A && nextStage is Stage.S) || ((int)nextStage == (int)stage + 1 && IsValid(width, height, stages, nextIndex, nextStage, direction));
    }
}