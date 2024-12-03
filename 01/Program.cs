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

leftCol.Sort();
rightCol.Sort();

var sum = 0;
for (var i = 0; i < leftCol.Count; ++i)
{
    var difference = Math.Abs(leftCol[i] - rightCol[i]);
    Console.WriteLine(difference);
    sum += difference;
}

Console.WriteLine(sum);
