var input = File.ReadAllText(args[0]);

var width = input.IndexOf('\n');

input = new string(input.Where(x => x != '\n').ToArray());

var length = input.Length / width;
var map = new int[width, length];

for (var y = 0; y < length; ++y)
{
    for (var x = 0; x < width; ++x)
    {
        map[x, y] = (int)char.GetNumericValue(input[y * width + x]);
    }
}

var score = 0;

for (var y = 0; y < length; ++y)
{
    for (var x = 0; x < width; ++x)
    {
        if (map[x, y] == 0)
        {
            score += FindTrail(map, (x, y)).Count;
        }
    }
}

Console.WriteLine(score);

static HashSet<(int x, int y)> FindTrail(int[,] map, (int x, int y) pos)
{
    var height = map[pos.x, pos.y];
    if (height == 9)
    {
        return [pos];
    }

    var result = new HashSet<(int x, int y)>();
    var targets = new[]
    {
        (pos.x, pos.y - 1),
        (pos.x + 1, pos.y),
        (pos.x, pos.y + 1),
        (pos.x - 1, pos.y)
    };

    foreach (var target in targets)
    {
        if (map.TryGet(target) == height + 1)
        {
            result.UnionWith(FindTrail(map, target));
        }
    }

    return result;
}

static class ArrayExtensions
{
    public static T TryGet<T>(this T[,] array, (int x, int y) pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= array.GetLength(0) || pos.y >= array.GetLength(1)) return default;

        return array[pos.x, pos.y];
    }
}