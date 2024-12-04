using System.Text.RegularExpressions;

var input = File.ReadAllText(args[0]);
input = Regex.Replace(input, @"don't\(\).*?do\(\)", string.Empty, RegexOptions.Singleline);
input = Regex.Replace(input, @"don't\(\).*", string.Empty, RegexOptions.Singleline);

var result = 0;
var matches = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)");

foreach (Match match in matches)
{
    var a = int.Parse(match.Groups[1].Value);
    var b = int.Parse(match.Groups[2].Value);
    result += a * b;
}

Console.WriteLine(result);