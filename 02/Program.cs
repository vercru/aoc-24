using var sr = new StreamReader(args[0]);

var totalSafe = 0;
while (sr.Peek() != -1)
{
    var line = sr.ReadLine();
    var lineSplit = line!.Split(' ');
    var report = lineSplit.Select(x => int.Parse(x)).ToList();

    var trend = 0;
    var levelsDifferBreak = false;
    for (var i = 0; i < report.Count - 1; ++i)
    {
        var curr = report[i];
        var next = report[i + 1];

        trend += next > curr ? 1 : next < curr ? -1 : 0;

        var diff = Math.Abs(next - curr);
        if (diff < 1 || diff > 3)
        {
            Console.WriteLine($"Unsafe diff {diff}");
            levelsDifferBreak = true;
            break;
        }
    }

    if (levelsDifferBreak) continue;

    if (Math.Abs(trend) != report.Count - 1)
    {
        Console.WriteLine("Unsafe trend");
        continue;
    }

    Console.WriteLine("Safe");
    ++totalSafe;
}

Console.WriteLine(totalSafe);