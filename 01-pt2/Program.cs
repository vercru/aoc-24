using var sr = new StreamReader(args[0]);

var leftCol = new List<int>();
var rightCol = new List<int>();

while (sr.Peek() != -1)
{
    var line = sr.ReadLine();
    var lineSplit = line!.Split(' ');
    leftCol.Add(int.Parse(lineSplit[0]));
    rightCol.Add(int.Parse(lineSplit[^1]));
}

var sum = 0;
foreach (var entry in leftCol)
{
    var occurrences = rightCol.Where(x => x == entry).Count();
    var score = entry * occurrences;
    Console.WriteLine(score);
    sum += score;
}

Console.WriteLine(sum);
