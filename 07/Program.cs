using System.Text.RegularExpressions;

var input = File.ReadAllText(args[0]);

var equations = input
    .Split("\n")
    .Where(x => x.Length > 0)
    .ToList();

var totalCalibrationResult = 0L;

foreach (var equation in equations)
{
    var matches = Regex.Matches(equation, @"\d+");
    var values = new List<long>();

    foreach (Match match in matches)
    {
        foreach (Group group in match.Groups)
        {
            values.Add(long.Parse(group.Value));
        }
    }

    var result = values[0];
    values.RemoveAt(0);

    var resultSet = new List<(int step, long result)> { (0, values[0]) };

    for (var i = 1; i < values.Count; ++i)
    {
        var intermResults = resultSet
            .Where(x => x.step == i - 1)
            .Select(x => x.result)
            .ToList();

        foreach (var intermResult in intermResults)
        {
            resultSet.Add((i, intermResult + values[i]));
            resultSet.Add((i, intermResult * values[i]));
        }
    }

    var truly = resultSet.Where(x => x.step == values.Count - 1).Any(x => x.result == result);

    if (truly)
    {
        totalCalibrationResult += result;
    }
}

Console.WriteLine(totalCalibrationResult);