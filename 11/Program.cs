var input = File.ReadAllText(args[0]);

var stones = input.Split(' ').Select(long.Parse).ToList();

for (var i = 0; i < 25; ++i)
{
    for (var j = 0; j < stones.Count; ++j)
    {
        var stone = stones[j];

        if (stone == 0)
        {
            stones[j] = 1;
        }
        else if ((Math.Floor(Math.Log10(stone)) + 1) % 2 == 0)
        {
            var strStone = stone.ToString();
            var firstHalf = long.Parse(strStone[..(strStone.Length / 2)]);
            var secondHalf = long.Parse(strStone[(strStone.Length / 2)..]);
            stones[j] = firstHalf;
            stones.Insert(++j, secondHalf);
        }
        else
        {
            stones[j] *= 2024;
        }
    }
}

Console.WriteLine(stones.Count);