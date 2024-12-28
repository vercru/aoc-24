var input = File.ReadAllText(args[0]);

var grid = input
    .Split("\n")
    .Select(x => x.ToCharArray())
    .Where(x => x.Any())
    .ToArray();

var length = grid.Length;
var width = grid[0].Length;

var guardPos = (x: 0, y: 0);

for (var y = 0; y < length; ++y)
{
    for (var x = 0; x < width; ++x)
    {
        if (grid[y][x] == '^')
        {
            guardPos = (x, y);
        }
    }
}

var directions = new []
{
    (x: 0, y: -1),
    (x: 1, y: 0),
    (x: 0, y: 1),
    (x: -1, y: 0)
};

var facingIndex = 0;

var visited = new List<(int x, int y)>
{
    guardPos
};

do
{
    var facing = directions[facingIndex];
    var target = (x: guardPos.x + facing.x, y: guardPos.y + facing.y);

    if (target.x == -1 || target.x == width || target.y == -1 || target.y == length)
    {
        break;
    }

    if (grid[target.y][target.x] == '#')
    {
        facingIndex = (facingIndex + 1) % directions.Length;
    }
    else
    {
        guardPos = target;
        if (!visited.Any(x => x.x == guardPos.x && x.y == guardPos.y))
        {
            visited.Add(guardPos);
        }
    }
}
while (true);

Console.WriteLine(visited.Count);