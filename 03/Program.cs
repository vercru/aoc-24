using System.Text.RegularExpressions;

using var sr = new StreamReader(args[0]);

var result = 0;
while (sr.Peek() != -1)
{
    var line = sr.ReadLine();

    var matches = Regex.Matches(line!, @"mul\((\d{1,3}),(\d{1,3})\)");
    foreach (Match match in matches)
    {
        var a = int.Parse(match.Groups[1].Value);
        var b = int.Parse(match.Groups[2].Value);
        result += a * b;
    }
}

Console.WriteLine(result);