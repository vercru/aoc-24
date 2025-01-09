var input = File.ReadAllText(args[0]);

var blinks = 75;
var stones = new List<IDictionary<long, long>>(blinks + 1);
for (var i = 0; i < blinks + 1; ++i)
{
    stones.Add(new Dictionary<long, long>());
}

stones[0] = input
    .Split(' ')
    .Select(long.Parse)
    .GroupBy(x => x)
    .ToDictionary(x => x.Key, x => x.LongCount());

for (var i = 0; i < blinks; ++i)
{
    var currStones = stones[i];
    var nextStones = stones[i + 1];
    foreach (var (stone, count) in currStones)
    {
        if (stone == 0)
        {
            if (nextStones.ContainsKey(1)) nextStones[1] += count;
            else nextStones.Add(1, count);
        }
        else if ((Math.Floor(Math.Log10(stone)) + 1) % 2 == 0)
        {
            var strStone = stone.ToString();
            var firstHalf = long.Parse(strStone[..(strStone.Length / 2)]);
            var secondHalf = long.Parse(strStone[(strStone.Length / 2)..]);
            if (nextStones.ContainsKey(firstHalf)) nextStones[firstHalf] += count;
            else nextStones.Add(firstHalf, count);
            if (nextStones.ContainsKey(secondHalf)) nextStones[secondHalf] += count;
            else nextStones.Add(secondHalf, count);
        }
        else
        {
            var newStone = stone * 2024;
            if (nextStones.ContainsKey(newStone)) nextStones[newStone] += count;
            else nextStones.Add(newStone, count);
        }
    }
}

Console.WriteLine(stones[stones.Count - 1].Sum(x => x.Value));