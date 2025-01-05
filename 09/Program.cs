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

for (var j = 0; j < compactedDisk.Count; ++j)
{
    if (compactedDisk[j] == -1)
    {
        var lastFileIdIdx = compactedDisk.FindLastIndex(x => x > -1);

        if (lastFileIdIdx < j)
        {
            break;
        }

        compactedDisk[j] = compactedDisk[lastFileIdIdx];
        compactedDisk[lastFileIdIdx] = -1;
    }
}

var checksum = compactedDisk
    .Where(x => x > -1)
    .Select((val, idx) => (val, idx))
    .Aggregate(0L, (a, b) => a + (b.val * b.idx));

Console.WriteLine(checksum);