var input = File.ReadAllText(args[0]);

var split = input.Split("\n\n").ToList();

var ordering = split[0].Split('\n').Select(x => (preceding: int.Parse(x[..2]), following: int.Parse(x[3..]))).ToList();

var updates = split[1]
    .Split('\n')
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Select(x => x
        .Split(',')
        .Select(y => int.Parse(y))
        .ToList())
    .ToList();

var result = 0;

foreach (var update in updates)
{
    var skip = true;

    for (var i = 0; i < update.Count - 1; i++)
    {
        var preceding = update[i];
        var following = update[i+1];

        if (ordering.Any(x => x.preceding == following && x.following == preceding))
        {
            update[i] = following;
            update[i+1] = preceding;
            i = -1;
            skip = false;
        }
    }

    if (!skip)
    {
        result += update[update.Count / 2];
    }
}

Console.WriteLine(result);