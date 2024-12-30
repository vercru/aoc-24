var input = File.ReadAllText(args[0]);

var width = input.IndexOf('\n');
var length = input.Split('\n').Where(x => x.Length > 0).Count();

var grid = new Dictionary<char, IList<(int x, int y)>>();
var inputSingleFile = input.Replace("\n", string.Empty);

for (var y = 0; y < length; ++y)
{
    for (var x = 0; x < width; ++x)
    {
        var key = inputSingleFile[y * width + x];
        if (grid.TryGetValue(key, out var positions))
        {
            positions.Add((x, y));
        }
        else
        {
            grid.Add(key, [(x, y)]);
        }

    }
}

var antinodePositions = new List<(int x, int y)>();

foreach (var (key, positions) in grid.Where(x => x.Key != '.' && x.Value.Count > 1))
{
    foreach (var position in positions)
    {
        var otherPositions = positions.Where(x => x != position).ToList();

        foreach (var otherPosition in otherPositions)
        {
            var antinode = (x: position.x + (otherPosition.x - position.x) * 2, y: position.y + (otherPosition.y - position.y) * 2);

            if (WithinBounds(antinode, (0, 0), (width, length)) && !antinodePositions.Contains(antinode))
            {
                antinodePositions.Add(antinode);
            }
        }
    }
}

Console.WriteLine(antinodePositions.Count);

static bool WithinBounds((int x, int y) pos, (int x, int y) min, (int x, int y) max)
{
    return pos.x >= min.x && pos.x < max.x && pos.y >= min.y && pos.y < max.y;
}