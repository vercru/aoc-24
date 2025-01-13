var input = File.ReadAllText(args[0]);
var strippedInput = new string(input.Where(x => x != '\n').ToArray());

var width = input.IndexOf('\n');
var length = strippedInput.Length / width;

var regions = new List<(
    char plant,
    ISet<(int x, int y)> area,
    ISet<((int x, int y) from, (int x, int y) to, (int x, int y) facing)> fences
)>();

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
            regions.Add((plant, new HashSet<(int, int)> { (x, y) }, new HashSet<((int, int), (int, int), (int, int))>()));
        }
    }
}

var totalPrice = 0L;

foreach (var region in regions)
{
    foreach (var pos in region.area)
    {
        if (!region.area.Contains((pos.x - 1, pos.y)))
        {
            region.fences.Add(((pos.x, pos.y), (pos.x, pos.y + 1), (1, 0)));
        }

        if (!region.area.Contains((pos.x, pos.y - 1)))
        {
            region.fences.Add(((pos.x, pos.y), (pos.x + 1, pos.y), (0, 1)));
        }

        if (!region.area.Contains((pos.x + 1, pos.y)))
        {
            region.fences.Add(((pos.x + 1, pos.y), (pos.x + 1, pos.y + 1), (-1, 0)));
        }

        if (!region.area.Contains((pos.x, pos.y + 1)))
        {
            region.fences.Add(((pos.x, pos.y + 1), (pos.x + 1, pos.y + 1), (0, -1)));
        }
    }

    foreach (var fence in region.fences.OrderBy(x => x.from.y).ThenBy(x => x.from.x))
    {
        var fenceClone = fence;

        if (fence.from.x == fence.to.x)
        {
            while (true)
            {
                var otherFence = region.fences
                    .Where(x => x != fenceClone)
                    .Where(x => x.facing == fence.facing)
                    .Where(x => x.from.x == fenceClone.from.x)
                    .Where(x => x.to.x == fenceClone.to.x)
                    .Where(x => x.from.y == fenceClone.to.y)
                    .SingleOrDefault();

                if (otherFence == default)
                {
                    break;
                }

                region.fences.Remove(fenceClone);
                region.fences.Remove(otherFence);

                fenceClone.to.y = otherFence.to.y;

                region.fences.Add(fenceClone);
            }
        }
        else
        {
            while (true)
            {
                var otherFence = region.fences
                    .Where(x => x != fenceClone)
                    .Where(x => x.facing == fence.facing)
                    .Where(x => x.from.y == fenceClone.from.y)
                    .Where(x => x.to.y == fenceClone.to.y)
                    .Where(x => x.from.x == fenceClone.to.x)
                    .SingleOrDefault();

                if (otherFence == default)
                {
                    break;
                }

                region.fences.Remove(fenceClone);
                region.fences.Remove(otherFence);

                fenceClone.to.x = otherFence.to.x;

                region.fences.Add(fenceClone);
            }
        }
    }

    totalPrice += region.area.Count * region.fences.Count;
}

Console.WriteLine(totalPrice);