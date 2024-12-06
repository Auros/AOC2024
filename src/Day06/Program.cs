var data = File.ReadAllLines("Input.txt");

var width = data[0].Length;
var height = data.Length;

Span<Cell> table = stackalloc Cell[width * height];
var guardDirection = Direction.Up;
Position guardPosition = default;
int visitedCellCount = 1; // start position always counts as 1

// Load in data
for (int y = 0; y < height; y++)
{
    var line = data[y];
    for (int x = 0; x < width; x++)
    {
        var index = y * width + x;
        var qq = table[index] = line[x] switch
        {
            '.' => Cell.Empty,
            '#' => Cell.Obstacle,
            '^' or '>' or '<' or 'v' => SetGuardIndex(new Position(x, y, width), out guardPosition),
            _ => throw new ArgumentOutOfRangeException()
        };

        _ = qq;
    }
}

// Calculate the thingy mabobber

while (AdvancePosition(table, ref guardPosition, ref guardDirection, width, height, out var cell) is not Cell.OutOfBounds)
{
    if (cell is not Cell.Empty)
        continue;
    
    // set as visited
    table[guardPosition.Index] = Cell.Visited;
    visitedCellCount++;
}


Console.WriteLine("Part 1: " + visitedCellCount);

static Cell SetGuardIndex(Position pos, out Position target)
{
    target = pos;
    return Cell.Visited;
}

static Cell AdvancePosition(ReadOnlySpan<Cell> table, ref Position guardPos, ref Direction guardDir, int width, int height, out Cell result)
{
    var (xAdditive, yAdditive) = guardDir switch
    {
        Direction.Up => (0, -1),
        Direction.Left => (-1, 0),
        Direction.Down => (0, 1),
        Direction.Right => (1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(guardDir), guardDir, null)
    };

    // Obstacle and out of bounds detection.
    Position newPos = new(guardPos.X + xAdditive, guardPos.Y + yAdditive, width);
    if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= width || newPos.Y >= height)
    {
        // out of bounds
        return result = Cell.OutOfBounds;
    }

    if (table[newPos.Index] is Cell.Obstacle)
    {
        // rotate to the right
        guardDir = (Direction)(((byte)guardDir + 1) % 4);
        return result = Cell.Obstacle;
    }

    guardPos = newPos;
    return result = table[guardPos.Index];
}

internal enum Cell : byte
{
    Empty = 0,
    Obstacle = 1,
    Visited = 2,
    OutOfBounds = 3
}

internal enum Direction : byte
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

internal readonly record struct Position(int X, int Y, int Width)
{
    public int Index => Y * Width + X;
}