using var sr = new StreamReader(args[0]);

var totalSafe = 0;
while (sr.Peek() != -1)
{
    var line = sr.ReadLine();
    var lineSplit = line!.Split(' ');
    var report = lineSplit.Select(x => int.Parse(x)).ToList();

    var safe = false;
    var omitLevelIndex = -1;
    while (!safe && omitLevelIndex < report.Count)
    {
        var innerReport = report.ToList();
        if (omitLevelIndex > -1)
        {
            innerReport.RemoveAt(omitLevelIndex);
        }
        ++omitLevelIndex;

        var trend = 0;
        var levelsDifferBreak = false;
        for (var i = 0; i < innerReport.Count - 1; ++i)
        {
            var curr = innerReport[i];
            var next = innerReport[i + 1];

            trend += next > curr ? 1 : next < curr ? -1 : 0;

            var diff = Math.Abs(next - curr);
            if (diff < 1 || diff > 3)
            {
                levelsDifferBreak = true;
                break;
            }
        }

        if (levelsDifferBreak) continue;

        if (Math.Abs(trend) != innerReport.Count - 1)
        {
            continue;
        }

        safe = true;
    }

    if (!safe)
    {
        Console.WriteLine("Unsafe");
        continue;
    }

    Console.WriteLine("Safe");
    ++totalSafe;
}

Console.WriteLine(totalSafe);