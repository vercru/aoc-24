var input = File.ReadAllText(args[0]);
var length = input.Count(x => x == '\n');
var width = input.Length / length;

var grid = new char[width, length];

for (var y = 0; y < length; y++)
{
    for (var x = 0; x < width; x++)
    {
        grid[x, y] = input[y * width + x];
    }
}

var cursorX = 0;
var cursorY = 0;
var hitCount = 0;

var offsets = new[] {
    (-1, -1), (-1, 1), (0, 0), (1, -1), (1, 1),
    (-1, -1), (1, -1), (0, 0), (-1, 1), (1, 1),
    (1, -1), (1, 1), (0, 0), (-1, 1), (-1, -1),
    (1, 1), (-1, 1), (0, 0), (-1, -1), (1, -1),
};

while (cursorX < width && cursorY < length)
{
    var offsetsIdx = 0;

    do
    {
        if (grid.TryGet(cursorX + offsets[offsetsIdx].Item1, cursorY + offsets[offsetsIdx].Item2) == 'M' &&
            grid.TryGet(cursorX + offsets[offsetsIdx+1].Item1, cursorY + offsets[offsetsIdx+1].Item2) == 'M' &&
            grid.TryGet(cursorX + offsets[offsetsIdx+2].Item1, cursorY + offsets[offsetsIdx+2].Item2) == 'A' &&
            grid.TryGet(cursorX + offsets[offsetsIdx+3].Item1, cursorY + offsets[offsetsIdx+3].Item2) == 'S' &&
            grid.TryGet(cursorX + offsets[offsetsIdx+4].Item1, cursorY + offsets[offsetsIdx+4].Item2) == 'S') ++hitCount;
        offsetsIdx += 5;
    }
    while (offsetsIdx < offsets.Length);

    ++cursorX;

    if (cursorX == width)
    {
        cursorX = 0;
        ++cursorY;
    }
}

Console.WriteLine(hitCount);

static class FooExtensions
{
    public static T? TryGet<T>(this T[,] array, int indexX, int indexY)
    {
        if (indexX < 0 || indexY < 0) return default;
        if (indexX >= array.GetLength(0) || indexY >= array.GetLength(1)) return default;

        return array[indexX, indexY];
    }
}