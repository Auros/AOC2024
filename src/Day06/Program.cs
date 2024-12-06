var data = File.ReadAllLines("Input.txt");

var width = data[0].Length;
var height = data.Length;

Span<Cell> realityMap = stackalloc Cell[width * height];
Span<Cell> virtualMap = stackalloc Cell[width * height]; // used for part 2: simulating the future obstacle placement
var guardDirection = Direction.Up;
Position guardPosition = default;
int visitedCellCount = 1; // start position always counts as 1
int infiniteLoopCount = 0;

// Load in data
for (int y = 0; y < height; y++)
{
    var line = data[y];
    for (int x = 0; x < width; x++)
    {
        var index = y * width + x;
        realityMap[index] = line[x] switch
        {
            '.' => Cell.Empty,
            '#' => Cell.Obstacle,
            '^' or '>' or '<' or 'v' => SetGuardIndex(new Position(x, y, width), out guardPosition),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

var previousPosition = guardPosition;
var previousDirection = guardDirection;

// Calculate the thingy mabobber
while (AdvancePosition(realityMap, ref guardPosition, ref guardDirection, width, height, out var cell) is not Cell.OutOfBounds)
{
    var lastPosition = previousPosition;
    var lastDirection = previousDirection;
    previousPosition = guardPosition;
    previousDirection = guardDirection;
    
    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (cell is Cell.Obstacle)
        continue;

    // set as visited
    realityMap[guardPosition.Index] = CellFromDirection(guardDirection) | realityMap[guardPosition.Index];
    
    if (cell is not Cell.Empty)
        continue;
    
    visitedCellCount++;
    
    realityMap.CopyTo(virtualMap);
    virtualMap[guardPosition.Index] = Cell.Obstacle;
    
    var virtualGuardPos = lastPosition;
    var virtualGuardDir = lastDirection;

    while (AdvancePosition(virtualMap, ref virtualGuardPos, ref virtualGuardDir, width, height, out cell) is not Cell.OutOfBounds)
    {
        var previous = virtualMap[virtualGuardPos.Index];
        var incomingCell = CellFromDirection(virtualGuardDir);
        var target = previous | incomingCell;
        
        // set as visited
        if (target == previous)
        {
            infiniteLoopCount++;
            break; // exit since uhh... infinite loop lol
        }
        virtualMap[virtualGuardPos.Index] = target;
    }
}

Console.WriteLine("Part 1: " + visitedCellCount);
Console.WriteLine("Part 2: " + infiniteLoopCount);

return;

static Cell SetGuardIndex(Position pos, out Position target)
{
    target = pos;
    return Cell.VisitedVerticalUp;
}

static Cell CellFromDirection(Direction direction)
{
    return direction switch
    {
        Direction.Left => Cell.VisitedHorizontalLeft,
        Direction.Right => Cell.VisitedHorizontalRight,
        Direction.Up => Cell.VisitedVerticalUp,
        _ => Cell.VisitedVerticalDown
    };
}

static bool GetNextCell(Position guardPos, Direction guardDir, int width, int height, out Position pos)
{
    var (xAdditive, yAdditive) = guardDir switch
    {
        Direction.Up => (0, -1),
        Direction.Left => (-1, 0),
        Direction.Down => (0, 1),
        Direction.Right => (1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(guardDir), guardDir, null)
    };
    
    // Out of bounds detection.
    Position newPos = new(guardPos.X + xAdditive, guardPos.Y + yAdditive, width);
    if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= width || newPos.Y >= height)
    {
        // out of bounds
        pos = newPos;
        return false;
    }

    pos = newPos;
    return true;
}

static Cell AdvancePosition(ReadOnlySpan<Cell> table, ref Position guardPos, ref Direction guardDir, int width, int height, out Cell result)
{
    // Out of bounds detection.
    if (!GetNextCell(guardPos, guardDir, width, height, out var pos))
    {
        // out of bounds
        return result = Cell.OutOfBounds;
    }
    
    // Obstacle detection
    if (table[pos.Index] is Cell.Obstacle)
    {
        // rotate to the right
        guardDir = (Direction)(((byte)guardDir + 1) % 4);
        return result = Cell.Obstacle;
    }

    // all good!
    guardPos = pos;
    return result = table[guardPos.Index];
}

[Flags]
internal enum Cell
{
    Empty = 0,
    Obstacle = 1,
    VisitedHorizontalLeft = 2,
    VisitedHorizontalRight = 4,
    VisitedVerticalUp = 8,
    VisitedVerticalDown = 16,
    OutOfBounds = 32
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