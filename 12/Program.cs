var input = File.ReadAllText(args[0]);
var strippedInput = new string(input.Where(x => x != '\n').ToArray());

var width = input.IndexOf('\n');
var length = strippedInput.Length / width;

var regions = new List<(char plant, ISet<(int x, int y)> area)>();

for (var y = 0; y < length; ++y)
{
    for (var x = 0; x < width; ++x)
    {
        var plant = strippedInput[y * width + x];

        var leftNeighbour = regions.SingleOrDefault(u => u.area.Contains((x - 1, y)));
        var topNeighbour = regions.SingleOrDefault(u => u.area.Contains((x, y - 1)));
        var createNewRegion = true;

        if (leftNeighbour.plant == plant)
        {
            leftNeighbour.area.Add((x, y));
            createNewRegion = false;
        }

        if (topNeighbour.plant == plant)
        {
            if (leftNeighbour.plant == plant && topNeighbour.area != leftNeighbour.area)
            {
                topNeighbour.area.UnionWith(leftNeighbour.area);
                regions.Remove(leftNeighbour);
            }
            else
            {
                topNeighbour.area.Add((x, y));
            }

            createNewRegion = false;
        }

        if (createNewRegion)
        {
            regions.Add((plant, new HashSet<(int, int)> { (x, y) }));
        }
    }
}

var totalPrice = 0L;

foreach (var region in regions)
{
    var perimiter = region.area.Sum(pos =>
        (region.area.Contains((pos.x - 1, pos.y)) ? 0 : 1) +
        (region.area.Contains((pos.x, pos.y - 1)) ? 0 : 1) +
        (region.area.Contains((pos.x + 1, pos.y)) ? 0 : 1) +
        (region.area.Contains((pos.x, pos.y + 1)) ? 0 : 1));

    totalPrice += region.area.Count * perimiter;
}

Console.WriteLine(totalPrice);