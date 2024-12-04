var data = File.ReadAllText("Input.txt").AsSpan();

var width = data.IndexOf('\n') - 1; // look for the first newline
var height = data.Count('\n') + 1; // count how many newlines + 1 for the last line

int xmasCount = 0;
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
    if (stages[i] is not Stage.X)
        continue;

    // Start looking at X and recursively scan neighbors
    foreach (var direction in directions)
        if (Searcher.IsValid(width, height, stages, i, Stage.X, direction))
            xmasCount++;
}

Console.WriteLine(xmasCount);

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
        if (0 > nextRowIndex || nextRowIndex == width || 0 > nextColumnIndex || nextColumnIndex == height)
            return false;
    
        var nextIndex = index + direction.X + direction.Y * width;
        var nextStage = stages[nextIndex]; // Find the next character in the word based on the direction

        return (stage is Stage.A && nextStage is Stage.S) || ((int)nextStage == (int)stage + 1 && IsValid(width, height, stages, nextIndex, nextStage, direction));
    }
}