var input = File.ReadAllText(args[0]);

var diskMap = input
    .Where(x => x != '\n')
    .Select(char.GetNumericValue)
    .ToList();

var disk = new List<int>();
var i = 0;
var fileId = 0;
while (i < diskMap.Count)
{
    var fileLength = diskMap[i++];

    for (var j = 0; j < fileLength; ++j)
    {
        disk.Add(fileId);
    }

    ++fileId;

    if (i == diskMap.Count)
    {
        break;
    }

    var freeSpaceLength = diskMap[i++];

    for (var j = 0; j < freeSpaceLength; ++j)
    {
        disk.Add(-1);
    }
}

var compactedDisk = disk.ToList();

for (var j = compactedDisk.Count - 1; j > -1; --j)
{
    if (compactedDisk[j] != -1)
    {
        var startIdx = compactedDisk.IndexOf(compactedDisk[j]);
        var fileLength = j - startIdx + 1;

        var emptyLength = 0;
        for (var k = 0; k < startIdx; ++k)
        {
            if (compactedDisk[k] == -1)
            {
                ++emptyLength;
                if (emptyLength == fileLength)
                {
                    for (var l = 0; l < fileLength; ++l)
                    {
                        compactedDisk[k - l] = compactedDisk[j - l];
                        compactedDisk[j - l] = -1;
                    }
                    break;
                }
            }
            else
            {
                emptyLength = 0;
            }
        }

        j = startIdx;
    }
}

var checksum = compactedDisk
    .Select(x => x == -1 ? 0 : x)
    .Select((val, idx) => (val, idx))
    .Aggregate(0L, (a, b) => a + (b.val * b.idx));

Console.WriteLine(checksum);